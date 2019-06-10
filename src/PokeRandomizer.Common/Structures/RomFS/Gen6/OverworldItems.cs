using System;
using System.Collections.Generic;
using System.Linq;
using pkNX.Structures;
using GameVersion = PokeRandomizer.Common.Game.GameVersion;

namespace PokeRandomizer.Common.Structures.RomFS.Gen6
{
	public class OverworldItems : IDataStructure
	{
		#region Game-Specific Constants

		private static readonly Dictionary<GameVersion, int> ScriptIdBases = new Dictionary<GameVersion, int> {
			{ GameVersion.XY, 7000 },
			{ GameVersion.ORAS, 7000 },
		};

		private static readonly Dictionary<GameVersion, int> DataPayloadOffsets = new Dictionary<GameVersion, int> {
			{ GameVersion.XY, 9 },
			{ GameVersion.ORAS, 9 },
		};

		private static readonly Dictionary<GameVersion, int> ItemCounts = new Dictionary<GameVersion, int> {
			{ GameVersion.XY, 207 },
			{ GameVersion.ORAS, 240 },
		};

		#endregion

		private readonly GameVersion gameVersion;

		private Amx amx;

		public OverworldItems( GameVersion gameVersion )
		{
			this.gameVersion = gameVersion;

			this.ValidateGameVersion();
		}

		public int                 ScriptIdBase { get; private set; }
		public List<OverworldItem> Items        { get; private set; }

		public IEnumerable<uint> ItemData => this.amx.DataPayload.Skip( DataPayloadOffsets[ this.gameVersion ] );

		private void ValidateGameVersion()
		{
			if ( !ScriptIdBases.ContainsKey( this.gameVersion ) ||
				 !DataPayloadOffsets.ContainsKey( this.gameVersion ) ||
				 !ItemCounts.ContainsKey( this.gameVersion ) )
			{
				throw new NotSupportedException( $"Overworld item editing is not supported in the game version \"{this.gameVersion}\"" );
			}
		}

		public void Read( byte[] data )
		{
			this.amx = new Amx( data );

			var itemData = this.ItemData.ToArray();

			if ( itemData.Length % 3 != 0 )
				throw new InvalidOperationException( $"Item data in script has invalid length \"{itemData.Length}\". Length must be divisible by 3." );
			if ( itemData.Length / 3 < ItemCounts[ this.gameVersion ] )
				throw new InvalidOperationException( $"Item data in script has invalid length \"{itemData.Length}\". Length must be at least {ItemCounts[ this.gameVersion ] * 3}." );

			this.ScriptIdBase = ScriptIdBases[ this.gameVersion ];
			this.Items        = new List<OverworldItem>();

			for ( var i = 0; i < ItemCounts[ this.gameVersion ]; i++ )
			{
				this.Items.Add( new OverworldItem {
					ItemId         = itemData[ i * 3 ],
					Unused1        = itemData[ i * 3 + 1 ],
					ScriptIdOffset = itemData[ i * 3 + 2 ],
				} );
			}
		}

		public byte[] Write()
		{
			var dataStart = this.amx.CodeLength / sizeof( uint );
			var newItemData = this.Items.SelectMany( item => new[] {
				item.ItemId,
				item.Unused1,
				item.ScriptIdOffset,
			} ).ToArray();
			var originalItemData = this.ItemData;
			var newDataPayload = this.amx.DataPayload
									 // Original 9 data values before item data
									 .Take( DataPayloadOffsets[ this.gameVersion ] )
									 // Plus our new item data
									 .Concat( newItemData )
									 // Plus the "unused slots" from the original file
									 .Concat( originalItemData.Skip( newItemData.Length ) );
			var newInstructions = this.amx.DecompressedInstructions
									  // Original instructions before the data payload
									  .Take( dataStart )
									  // Our new data payload from above
									  .Concat( newDataPayload )
									  .ToArray();

			var newInstructionData = PawnUtil.CompressScript( newInstructions );
			var newData = this.amx.Data
							  // Original file contents before Code section
							  .Take( this.amx.Header.COD )
							  // New compressed code/data from above
							  .Concat( newInstructionData )
							  .ToArray();

			// Write new size to the header
			BitConverter.GetBytes( newData.Length ).CopyTo( newData, 0 );

			return newData;
		}
	}

	public class OverworldItem
	{
		public uint ItemId         { get; set; }
		public uint Unused1        { get; set; }
		public uint ScriptIdOffset { get; set; }
	}
}