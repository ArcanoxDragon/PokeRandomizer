using System.Threading.Tasks;
using NUnit.Framework;
using PokeRandomizer.Common.Data;
using PokeRandomizer.Common.Structures.ExeFS.Common;

namespace PokeRandomizer.Tests.ORAS
{
	[TestFixture]
	public class TmHmTests
	{
		[Test]
		public async Task Tm02DragonClaw()
		{
			TmsHms tmsHms = await ORASConfig.GameConfig.GetTmsHms();
			ushort dragonClawId = tmsHms.TmIds[1]; // TM02 - Dragon Claw (array is 0-indexed)

			Assert.AreEqual(Moves.DragonClaw, (Move) dragonClawId, $"TM02 should be Dragon Claw but it is {( (Move) dragonClawId ).Name}");
		}

		[Test]
		public async Task Hm01Cut()
		{
			TmsHms tmsHms = await ORASConfig.GameConfig.GetTmsHms();
			ushort cutId = tmsHms.HmIds[0]; // TM02 - Dragon Claw (array is 0-indexed)

			Assert.AreEqual(Moves.Cut, (Move) cutId, $"HM01 should be Cut but it is {( (Move) cutId ).Name}");
		}
	}
}