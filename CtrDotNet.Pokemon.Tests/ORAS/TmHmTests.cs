using System.Threading.Tasks;
using CtrDotNet.Pokemon.Data;
using CtrDotNet.Pokemon.Structures.ExeFS.Common;
using NUnit.Framework;

namespace CtrDotNet.Pokemon.Tests.ORAS
{
	[ TestFixture ]
	public class TmHmTests
	{
		[ Test ]
		public void Tm02DragonClaw()
		{
			TmsHms tmsHms = ORASConfig.GameConfig.GetTmsHms();
			ushort dragonClawId = tmsHms.TmIds[ 1 ]; // TM02 - Dragon Claw (array is 0-indexed)

			Assert.AreEqual( Moves.DragonClaw, (Move) dragonClawId, $"TM02 should be Dragon Claw but it is {( (Move) dragonClawId ).Name}" );
		}

		[ Test ]
		public void Hm01Cut()
		{
			TmsHms tmsHms = ORASConfig.GameConfig.GetTmsHms();
			ushort cutId = tmsHms.HmIds[ 0 ]; // TM02 - Dragon Claw (array is 0-indexed)

			Assert.AreEqual( Moves.Cut, (Move) cutId, $"HM01 should be Cut but it is {( (Move) cutId ).Name}" );
		}
	}
}