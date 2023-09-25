using System;

namespace Simulation
{
	public class UnlockRewardRune : UnlockReward
	{
		public UnlockRewardRune(Rune rune)
		{
			this.rune = rune;
			rune.lootpackChance = 0f;
		}

		public override UnlockReward.RewardCategory rewardCategory
		{
			get
			{
				return UnlockReward.RewardCategory.IMPORTANT_THING;
			}
		}

		public string GetRuneId()
		{
			return this.rune.id;
		}

		public override void Give(Simulator sim, World world)
		{
			sim.AddRune(this.rune);
		}

		public override string GetRewardString()
		{
			return string.Format(LM.Get("UNLOCK_REWARD_RUNE"), LM.Get(this.rune.nameKey));
		}

		public override string GetRewardedString()
		{
			return string.Format(LM.Get("UNLOCK_REWARDED_RUNE"), LM.Get(this.rune.nameKey));
		}

		public Rune GetRune()
		{
			return this.rune;
		}

		private Rune rune;
	}
}
