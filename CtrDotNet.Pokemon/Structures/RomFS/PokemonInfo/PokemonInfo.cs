using CtrDotNet.Pokemon.Structures.RomFS.Common;
using CtrDotNet.Pokemon.Utility;

namespace CtrDotNet.Pokemon.Structures.RomFS.PokemonInfo
{
	public abstract class PokemonInfo : IDataStructure
	{
		protected byte[] Data { get; set; }

		public abstract void Read( byte[] data );
		public abstract byte[] Write();

		public abstract int HP { get; set; }
		public abstract int Attack { get; set; }
		public abstract int Defense { get; set; }
		public abstract int Speed { get; set; }
		public abstract int SpecialAttack { get; set; }
		public abstract int SpecialDefense { get; set; }

		public int[] Stats => new[] { this.HP, this.Attack, this.Defense, this.Speed, this.SpecialAttack, this.SpecialDefense };

		public abstract int EvHP { get; set; }
		public abstract int EvAttack { get; set; }
		public abstract int EvDefense { get; set; }
		public abstract int EvSpeed { get; set; }
		public abstract int EvSpecialAttack { get; set; }
		public abstract int EvSpecialDefense { get; set; }

		public abstract int[] Types { get; set; }
		public abstract int CatchRate { get; set; }
		public virtual int EvoStage { get; set; }
		public abstract int[] Items { get; set; }
		public abstract int Gender { get; set; }
		public abstract int HatchCycles { get; set; }
		public abstract int BaseFriendship { get; set; }
		public abstract int ExpGrowth { get; set; }
		public abstract int[] EggGroups { get; set; }
		public abstract int[] Abilities { get; set; }
		public abstract int EscapeRate { get; set; }
		public virtual int FormeCount { get; set; }
		protected internal virtual int FormStatsIndex { get; set; }
		public virtual int FormeSprite { get; set; }
		public abstract int BaseExp { get; set; }
		public abstract int Color { get; set; }

		/// <summary>
		/// Centimeters
		/// </summary>
		public virtual int Height { get; set; } = 0;

		/// <summary>
		/// Hectograms (100 grams)
		/// </summary>
		public virtual int Weight { get; set; } = 0;

		public double HeightM => this.Height / 100.0;
		public double WeightKg => this.Weight / 10.0;

		public bool[] TMHM { get; set; }
		public bool[] TypeTutors { get; set; }
		public bool[][] SpecialTutors { get; set; } = new bool[ 0 ][];

		protected static bool[] GetBits( byte[] data )
		{
			bool[] r = new bool[ 8 * data.Length ];
			for ( int i = 0; i < r.Length; i++ )
				r[ i ] = ( data[ i / 8 ] >> ( i & 7 ) & 0x1 ) == 1;
			return r;
		}

		protected static byte[] SetBits( bool[] bits )
		{
			byte[] data = new byte[ bits.Length / 8 ];
			for ( int i = 0; i < bits.Length; i++ )
				data[ i / 8 ] |= (byte) ( bits[ i ] ? 1 << ( i & 0x7 ) : 0 );
			return data;
		}

		// Data Manipulation
		public int FormeIndex( int species, int forme )
		{
			if ( forme <= 0 ) // no forme requested
				return species;
			if ( this.FormStatsIndex <= 0 ) // no formes present
				return species;
			if ( forme > this.FormeCount ) // beyond range of species' formes
				return species;

			return this.FormStatsIndex + forme - 1;
		}

		public int RandomGender
		{
			get
			{
				switch ( this.Gender )
				{
					case 255: // Genderless
						return 2;
					case 254: // Female
						return 1;
					case 0: // Male
						return 0;
					default:
						return (int) ( RandomUtil.UInt32() % 2 );
				}
			}
		}

		public bool HasFormes => this.FormeCount > 1;
		public int BaseStat => this.HP + this.Attack + this.Defense + this.Speed + this.SpecialAttack + this.SpecialDefense;
	}
}