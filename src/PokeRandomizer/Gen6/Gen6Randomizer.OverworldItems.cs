﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CtrDotNet.Utility.Extensions;
using PokeRandomizer.Common.Data;
using PokeRandomizer.Common.Game;
using PokeRandomizer.Common.Reference;
using PokeRandomizer.Common.Utility;
using PokeRandomizer.Progress;
using PokeRandomizer.Utility;

namespace PokeRandomizer.Gen6
{
	public partial class Gen6Randomizer
	{
		public virtual async Task RandomizeOverworldItems(Random taskRandom, ProgressNotifier progressNotifier, CancellationToken cancellationToken)
		{
			var config = ValidateAndGetConfig().OverworldItems;

			if (!config.RandomizeOverworldItems)
				return;

			await LogAsync($"======== Beginning Overworld Item randomization ========{Environment.NewLine}");
			progressNotifier?.NotifyUpdate(ProgressUpdate.StatusOnly("Randomizing overworld items..."));

			var availableItemIds = Legality.Items.GetValidOverworldItems(Game.Version)
										   .Except(Game.Version.IsXY() ? Legality.Items.TM_XY : Legality.Items.TM_ORAS)
										   .ToArray();
			var availableTmItemIds = Legality.Items.GetValidOverworldItems(Game.Version)
											 .Intersect(Game.Version.IsXY() ? Legality.Items.TM_XY : Legality.Items.TM_ORAS)
											 .ToArray();
			var overworldItems = await Game.GetOverworldItems();
			var itemNames = ( await Game.GetTextFile(TextNames.ItemNames) ).Lines;

			if (!config.AllowMasterBalls)
				availableItemIds = availableItemIds.Except(new[] { (ushort) Items.MasterBall.Id }).ToArray();
			if (!config.AllowMegaStones)
				availableItemIds = availableItemIds.Except(Legality.Items.MegaStones).ToArray();

			foreach (var (i, item) in overworldItems.Items.Pairs())
			{
				progressNotifier?.NotifyUpdate(ProgressUpdate.Update(progressNotifier.Status, i / (double) overworldItems.Items.Count));

				var hmItems = Game.Version.IsXY() ? Legality.Items.HM_XY : Legality.Items.HM_ORAS;

				if (hmItems.Any(id => id == item.ItemId))
				{
					// Don't randomize HMs! Might strand the player!
					continue;
				}

				var oldItemId = item.ItemId;
				var isTM = availableTmItemIds.Contains((ushort) oldItemId);

				if (isTM)
				{
					if (config.RandomizeTMs)
					{
						item.ItemId = availableTmItemIds.GetRandom(taskRandom);
					}
				}
				else
				{
					var newItemId = availableItemIds.GetRandom(taskRandom);

					if (item.ItemId != Items.MasterBall.Id) // OR/AS only: Don't get rid of possibly the only Master Ball in the game!
						item.ItemId = newItemId;
				}

				if (item.ItemId != oldItemId)
				{
					var oldItemName = itemNames[(int) oldItemId];
					var newItemName = itemNames[(int) item.ItemId];

					await LogAsync($"Changing {oldItemName.Article()} {oldItemName} to {newItemName.Article()} {newItemName}");
				}
			}

			await Game.SaveOverworldItems(overworldItems);
			await LogAsync($"{Environment.NewLine}======== Finished Overworld Item randomization ========{Environment.NewLine}");
		}
	}
}