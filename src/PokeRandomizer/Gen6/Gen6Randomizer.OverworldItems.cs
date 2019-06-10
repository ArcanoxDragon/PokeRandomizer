using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CtrDotNet.Utility.Extensions;
using PokeRandomizer.Common.Data;
using PokeRandomizer.Progress;
using PokeRandomizer.Utility;

namespace PokeRandomizer.Gen6
{
	public partial class Gen6Randomizer
	{
		public virtual async Task RandomizeOverworldItems( ProgressNotifier progressNotifier, CancellationToken cancellationToken )
		{
			var config = this.ValidateAndGetConfig().OverworldItems;

			if ( !config.RandomizeOverworldItems )
				return;

			progressNotifier?.NotifyUpdate( ProgressUpdate.StatusOnly( "Randomizing overworld items..." ) );

			var availableItemIds = Legality.Items.GetValidOverworldItems( this.Game.Version );
			var overworldItems   = await this.Game.GetOverworldItems();

			if ( !config.AllowMasterBalls )
				availableItemIds = availableItemIds.Except( new[] { (ushort) Items.MasterBall.Id } ).ToArray();
			if ( !config.AllowTMs )
				availableItemIds = availableItemIds.Except( Legality.Items.TM_ORAS ).Except( Legality.Items.TM_XY ).ToArray();
			if ( !config.AllowMegaStones )
				availableItemIds = availableItemIds.Except( Legality.Items.MegaStones ).ToArray();

			foreach ( var (i, item) in overworldItems.Items.Pairs() )
			{
				progressNotifier?.NotifyUpdate( ProgressUpdate.Update( progressNotifier.Status, i / (double) overworldItems.Items.Count ) );

				item.ItemId = availableItemIds.GetRandom( this.Random );
			}

			await this.Game.SaveOverworldItems( overworldItems );
		}
	}
}