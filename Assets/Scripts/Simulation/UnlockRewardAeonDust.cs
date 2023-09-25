using System;
using Ui;

namespace Simulation
{
	public class UnlockRewardAeonDust : UnlockReward
	{
		public UnlockRewardAeonDust(double amount)
		{
			this.amount = amount;
		}

		public override UnlockReward.RewardCategory rewardCategory
		{
			get
			{
				return UnlockReward.RewardCategory.CURRENCY;
			}
		}

		public double GetAmount()
		{
			return this.amount;
		}

		public override void Give(Simulator sim, World world)
		{
			sim.riftQuestDustCollected += this.amount;
		}

		public override string GetRewardString()
		{
			return string.Format(LM.Get("RIFT_AEON_DUSTS"), GameMath.GetDoubleString(this.amount));
		}

		public override string GetRewardedString()
		{
			return string.Format(LM.Get("RIFT_AEON_DUSTS"), GameMath.GetDoubleString(this.amount));
		}

		protected double amount;

		public UiState uiState;

		public DropPosition dropPosition;
	}
}
