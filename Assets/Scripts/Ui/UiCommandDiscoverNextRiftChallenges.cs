using System;
using System.Collections.Generic;
using Simulation;
using Static;

namespace Ui
{
	public class UiCommandDiscoverNextRiftChallenges : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			if (sim.IsNextRiftsDiscoverable())
			{
				sim.DiscoverNextSetOfRifts();
				int latestUnlockedRiftChallengeIndex = sim.GetWorld(GameMode.RIFT).GetLatestUnlockedRiftChallengeIndex();
				World world = sim.GetWorld(GameMode.RIFT);
				world.SetActiveChallengeByIndex(latestUnlockedRiftChallengeIndex, false);
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiButtonDiscoverNewGates, 1f));
				PlayfabManager.SendPlayerEvent(PlayfabEventId.GOG_NEW_GATES_DISCOVERED, new Dictionary<string, object>
				{
					{
						"index",
						sim.riftDiscoveryIndex
					},
					{
						"num_gates_completed",
						PlayerStats.numRiftWon
					}
				}, null, null, true);
			}
		}
	}
}
