using System;

namespace Simulation
{
	public class ChallengeUpgradeHealth : ChallengeUpgrade
	{
		public ChallengeUpgradeHealth(double multiplier)
		{
			this.multiplier = multiplier;
		}

		public override void Apply(World world, ChallengeUpgradesTotal worldUpgradesTotal)
		{
			worldUpgradesTotal.healthFactor *= (this.multiplier - 1.0) * world.universalBonus.milestoneBonusFactor + 1.0;
			foreach (Hero hero in world.heroes)
			{
				if (hero.IsAlive())
				{
					hero.HealWithoutCallback((this.multiplier - 1.0) * world.universalBonus.milestoneBonusFactor);
				}
				hero.UpdateData();
			}
		}

		public override string GetDescription(World world)
		{
			return string.Format(LM.Get("CHALLENGE_UPGRADE_HEALTH"), GameMath.GetPercentString((this.multiplier - 1.0) * world.universalBonus.milestoneBonusFactor, true));
		}

		public double multiplier;
	}
}
