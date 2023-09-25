using System;

namespace Simulation
{
	public class UnlockRewardGameModeRift : UnlockRewardGameMode
	{
		public override void Give(Simulator sim, World world)
		{
			sim.UnlockGameMode(GameMode.RIFT);
			SoundArchieve.inst.LoadUiBundle("sounds/rift-mode");
		}

		public override string GetRewardString()
		{
			return LM.Get("UNLOCK_REWARD_RIFT");
		}

		public override string GetRewardedString()
		{
			return LM.Get("UNLOCK_REWARDED_RIFT");
		}
	}
}
