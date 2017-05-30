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
			Starters starters = await ORASConfig.GameConfig.GetStarters();

			starters[ 3 ][ 0 ] = (ushort) Species.Bulbasaur.Id;
			starters[ 3 ][ 1 ] = (ushort) Species.Charmander.Id;
			starters[ 3 ][ 2 ] = (ushort) Species.Squirtle.Id;

			ORASConfig.GameConfig.OutputPathOverride = this.path;

			await ORASConfig.GameConfig.SaveStarters( starters );

			ORASConfig.GameConfig.OutputPathOverride = null;
		}
	}
}