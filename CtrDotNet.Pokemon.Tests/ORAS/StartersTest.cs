using System.IO;
using System.Threading.Tasks;
using CtrDotNet.Pokemon.Cro;
using CtrDotNet.Pokemon.Data;
using CtrDotNet.Pokemon.Reference;
using CtrDotNet.Pokemon.Structures.CRO.Gen6.Starters;
using NUnit.Framework;

namespace CtrDotNet.Pokemon.Tests.ORAS
{
	[ TestFixture ]
	public class StartersTest
	{
		private readonly string path;

		public StartersTest()
		{
			this.path = Path.Combine( TestContext.CurrentContext.TestDirectory, "Edited", "RomFS" );

			if ( !Directory.Exists( this.path ) )
				Directory.CreateDirectory( this.path );
		}

		[ Test ]
		public async Task TestEditStarters()
		{
			CroFile dllField = await ORASConfig.GameConfig.GetCroFile( CroNames.Field );
			CroFile dllPokeSelect = await ORASConfig.GameConfig.GetCroFile( CroNames.Poke3Select );
			Starters starters = await ORASConfig.GameConfig.GetStarters();

			starters[ 3 ][ 0 ] = (ushort) Species.Bulbasaur.Id;
			starters[ 3 ][ 1 ] = (ushort) Species.Charmander.Id;
			starters[ 3 ][ 2 ] = (ushort) Species.Squirtle.Id;

			await starters.Write( dllField, dllPokeSelect );

			var fieldPath = Path.Combine( this.path, Path.GetFileName( dllField.Path ) );
			var pokePath = Path.Combine( this.path, Path.GetFileName( dllPokeSelect.Path ) );

			using ( var fs = new FileStream( fieldPath, FileMode.Create, FileAccess.Write, FileShare.None ) )
			{
				byte[] data = dllField.Write();
				await fs.WriteAsync( data, 0, data.Length );
			}

			using ( var fs = new FileStream( pokePath, FileMode.Create, FileAccess.Write, FileShare.None ) )
			{
				byte[] data = dllPokeSelect.Write();
				await fs.WriteAsync( data, 0, data.Length );
			}
		}
	}
}