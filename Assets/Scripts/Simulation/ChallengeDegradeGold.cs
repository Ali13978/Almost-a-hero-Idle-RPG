using System;

namespace Simulation
{
	public class ChallengeDegradeGold : ChallengeUpgrade
	{
		public ChallengeDegradeGold(double multiplier)
		{
			this.multiplier = multiplier;
		}

		public override void Apply(World world, ChallengeUpgradesTotal worldUpgradesTotal)
		{
			worldUpgradesTotal.goldFactor *= this.multiplier;
		}

		public override string GetDescription(World world)
		{
			return string.Format(LM.Get("CHALLENGE_DEGRADE_GOLD"), GameMath.GetPercentString((float)(this.multiplier - 1.0), true));
		}

		public double multiplier;
	}
}
