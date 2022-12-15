using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using CtrDotNet.CTR.Cro;
using NUnit.Framework;
using PokeRandomizer.Common.Reference;
using PokeRandomizer.Common.Structures.CRO.Gen6.Starters;

namespace PokeRandomizer.Tests.ORAS.Rewrite
{
	[TestFixture]
	public class Cro
	{
		private readonly string path;

		public Cro()
		{
			this.path = Path.Combine(ORASConfig.OutputPath, "RomFS");

			if (!Directory.Exists(this.path))
				Directory.CreateDirectory(this.path);
		}

		#region Helpers

		private async Task TestCroFile(CroFile cro, Func<Task> saveAction, bool failOnBadHash = true)
		{
			var fname = Path.GetFileName(cro.Path);
			var outPath = Path.Combine(this.path, fname);
			var origPath = Path.Combine(this.path, $"{Path.GetFileNameWithoutExtension(outPath)}.orig.cro");
			byte[] origData = await cro.Write();

			await saveAction();

			byte[] newData = await cro.Write();

			using var md5 = MD5.Create();
			await using var origFs = new FileStream(origPath, FileMode.Create, FileAccess.Write, FileShare.None);
			await using var newFs = new FileStream(outPath, FileMode.Create, FileAccess.Write, FileShare.None);
			await origFs.WriteAsync(origData, 0, origData.Length);
			await newFs.WriteAsync(newData, 0, newData.Length);

			byte[] hashOriginal = md5.ComputeHash(origData);
			byte[] hashNew = md5.ComputeHash(newData);

			try
			{
				Assert.AreEqual(hashOriginal, hashNew, $"Hash for rewritten {fname} did not match original");
			}
			catch (AssertionException ex)
			{
				if (failOnBadHash)
					throw;

				throw new InconclusiveException(ex.Message, ex);
			}
		}

		#endregion

		[Test]
		public async Task RewriteStarters()
		{
			var dllField = await ORASConfig.GameConfig.GetCroFile(CroNames.Field);
			var dllPoke3Select = await ORASConfig.GameConfig.GetCroFile(CroNames.Poke3Select);

			await TestCroFile(dllField, () => TestCroFile(dllPoke3Select, async () => {
				Starters starters = await ORASConfig.GameConfig.GetStarters();

				await ORASConfig.GameConfig.SaveStarters(starters, dllField, dllPoke3Select);
			}));
		}
	}
}