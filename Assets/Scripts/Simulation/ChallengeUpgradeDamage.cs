using System;

namespace Simulation
{
	public class ChallengeUpgradeDamage : ChallengeUpgrade
	{
		public ChallengeUpgradeDamage(double multiplier)
		{
			this.multiplier = multiplier;
		}

		public override void Apply(World world, ChallengeUpgradesTotal worldUpgradesTotal)
		{
			worldUpgradesTotal.heroDamageFactor *= (this.multiplier - 1.0) * world.universalBonus.milestoneBonusFactor + 1.0;
			foreach (Hero hero in world.heroes)
			{
				hero.UpdateData();
			}
		}

		public override string GetDescription(World world)
		{
			return string.Format(LM.Get("CHALLENGE_UPGRADE_DAMAGE"), GameMath.GetPercentString((this.multiplier - 1.0) * world.universalBonus.milestoneBonusFactor, true));
		}

		public double multiplier;
	}
}
