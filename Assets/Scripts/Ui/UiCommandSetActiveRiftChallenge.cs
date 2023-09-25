using System;
using Simulation;

namespace Ui
{
	public class UiCommandSetActiveRiftChallenge : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			World world = sim.GetWorld(GameMode.RIFT);
			if (this.isCursed)
			{
				world.activeChallenge = (world.cursedChallenges[this.challengeIndex] as ChallengeRift);
			}
			else
			{
				if (!world.IsRiftChallengeUnlocked(this.challengeIndex, sim.riftDiscoveryIndex))
				{
					return;
				}
				ChallengeRift challengeRift = world.allChallenges[this.challengeIndex] as ChallengeRift;
				if (challengeRift != null)
				{
					world.activeChallenge = challengeRift;
				}
			}
		}

		public int challengeIndex;

		public bool isCursed;
	}
}
