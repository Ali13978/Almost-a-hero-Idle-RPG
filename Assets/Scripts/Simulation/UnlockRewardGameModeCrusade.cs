using System;

namespace Simulation
{
	public class UnlockRewardGameModeCrusade : UnlockRewardGameMode
	{
		public override void Give(Simulator sim, World world)
		{
			sim.UnlockGameMode(GameMode.CRUSADE);
			SoundArchieve.inst.LoadUiBundle("sounds/timechallenge-mode");
		}

		public override string GetRewardString()
		{
			return LM.Get("UNLOCK_REWARD_TIME_CHALLENGE");
		}

		public override string GetRewardedString()
		{
			return LM.Get("UNLOCK_REWARDED_TIME_CHALLENGE");
		}
	}
}
