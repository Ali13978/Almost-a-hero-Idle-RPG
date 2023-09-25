using System;

namespace Simulation
{
	public class UnlockRewardTotem : UnlockReward
	{
		public UnlockRewardTotem(TotemDataBase totem)
		{
			this.totem = totem;
		}

		public override UnlockReward.RewardCategory rewardCategory
		{
			get
			{
				return UnlockReward.RewardCategory.IMPORTANT_THING;
			}
		}

		public string GetTotemId()
		{
			return this.totem.id;
		}

		public override void Give(Simulator sim, World world)
		{
			sim.UnlockTotem(this.totem.id);
		}

		public override string GetRewardString()
		{
			return string.Format(LM.Get("UNLOCK_REWARD_RING"), this.totem.GetName());
		}

		public override string GetRewardedString()
		{
			return string.Format(LM.Get("UNLOCK_REWARDED_RING"), this.totem.GetName());
		}

		private TotemDataBase totem;
	}
}
