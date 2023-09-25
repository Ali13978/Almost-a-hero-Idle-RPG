using System;

namespace Simulation
{
	public class ChallengeUpgradeGold : ChallengeUpgrade
	{
		public ChallengeUpgradeGold(double multiplier)
		{
			this.multiplier = multiplier;
		}

		public override void Apply(World world, ChallengeUpgradesTotal worldUpgradesTotal)
		{
			worldUpgradesTotal.goldFactor *= (this.multiplier - 1.0) * world.universalBonus.milestoneBonusFactor + 1.0;
		}

		public override string GetDescription(World world)
		{
			return string.Format(LM.Get("CHALLENGE_UPGRADE_GOLD"), GameMath.GetPercentString((this.multiplier - 1.0) * world.universalBonus.milestoneBonusFactor, true));
		}

		public double multiplier;
	}
}
