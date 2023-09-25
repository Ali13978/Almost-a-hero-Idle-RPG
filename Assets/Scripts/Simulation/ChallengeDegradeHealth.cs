using System;

namespace Simulation
{
	public class ChallengeDegradeHealth : ChallengeUpgrade
	{
		public ChallengeDegradeHealth(double multiplier)
		{
			this.multiplier = multiplier;
		}

		public override void Apply(World world, ChallengeUpgradesTotal worldUpgradesTotal)
		{
			worldUpgradesTotal.healthFactor *= this.multiplier;
			foreach (Hero hero in world.heroes)
			{
				if (hero.IsAlive())
				{
					hero.HealWithoutCallback(this.multiplier - 1.0);
				}
				hero.UpdateData();
			}
		}

		public override string GetDescription(World world)
		{
			return string.Format(LM.Get("CHALLENGE_DEGRADE_HEALTH"), GameMath.GetPercentString((float)(this.multiplier - 1.0), true));
		}

		public double multiplier;
	}
}
