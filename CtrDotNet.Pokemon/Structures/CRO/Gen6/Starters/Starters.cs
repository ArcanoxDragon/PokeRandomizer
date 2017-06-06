using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CtrDotNet.CTR.Cro;
using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Utility;

namespace CtrDotNet.Pokemon.Structures.CRO.Gen6.Starters
{
	public class Starters
	{
		private readonly StartersField startersField;
		private readonly StartersPokeSelect startersPokeSelect;

		public Starters( GameVersion gameVersion )
		{
			this.GameVersion = gameVersion;
			this.startersField = new StartersField( gameVersion );
			this.startersPokeSelect = new StartersPokeSelect( gameVersion );
		}

		/// <summary>
		/// array of starters for a given generation
		/// </summary>
		/// <param name="generation">The 1-indexed game generation to get starters for</param>
		public ushort[] this[ int generation ] => this.StarterSpecies[ generation - 1 ];

		public IEnumerable<int> Generations => this.startersPokeSelect.Generations;

		public GameVersion GameVersion { get; }

		/// <summary>
		/// 0-indexed array of starters for each generation
		/// </summary>
		public ushort[][] StarterSpecies
		{
			get
			{
				this.SynchronizeSpecies();
				return this.startersPokeSelect.StarterSpecies;
			}
		}

		private void SynchronizeSpecies()
		{
			// synchronize them so changes to one make it into the other
			this.startersPokeSelect.StarterSpecies.ForEach( ( arr, gen ) => this.startersField.StarterSpecies[ gen ] = arr );
		}

		public async Task Read( CroFile dllField, CroFile dllPoke3Select )
		{
			byte[] fieldCode = await dllField.GetCodeSection();
			byte[] pokeSelectData = await dllPoke3Select.GetDataSection();

			fieldCode.WithReader( br => this.startersField.ReadData( br ) );
			pokeSelectData.WithReader( br => this.startersPokeSelect.ReadData( br ) );

			for ( int gen = 1; gen <= 6; gen++ )
			for ( int entry = 0; entry < 3; entry++ )
			{
				ushort speciesField = this.startersField.StarterSpecies[ gen - 1 ][ entry ];
				ushort speciesPokeSelect = this.startersPokeSelect.StarterSpecies[ gen - 1 ][ entry ];

				if ( speciesField != speciesPokeSelect )
					throw new InvalidDataException( $"Species ID for Gen {gen}, Starter {entry + 1} did not match in the two CRO files. " +
													$"DllField was {speciesField} but DllPoke3Select was {speciesPokeSelect}" );
			}
		}

		public async Task Write( CroFile dllField, CroFile dllPoke3Select )
		{
			byte[] fieldCode = await dllField.GetCodeSection();
			byte[] pokeSelectData = await dllPoke3Select.GetDataSection();

			this.SynchronizeSpecies();

			fieldCode.WithWriter( bw => this.startersField.WriteData( bw ) );
			pokeSelectData.WithWriter( bw => this.startersPokeSelect.WriteData( bw ) );

			await dllField.WriteCodeSection( fieldCode );
			await dllPoke3Select.WriteDataSection( pokeSelectData );
		}
	}
}