using System;
using Simulation;

namespace Ui
{
	public class UiCommandAbandonChallenge : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			Challenge activeChallenge = sim.GetActiveWorld().activeChallenge;
			if (!(activeChallenge is ChallengeWithTime))
			{
				return;
			}
			ChallengeWithTime challengeWithTime = (ChallengeWithTime)activeChallenge;
			challengeWithTime.Abandon();
		}
	}
}
