using System;

namespace Simulation
{
	public class ChallengeUpgradeDamageTotem : ChallengeUpgrade
	{
		public ChallengeUpgradeDamageTotem(double multiplier)
		{
			this.multiplier = multiplier;
		}

		public override void Apply(World world, ChallengeUpgradesTotal worldUpgradesTotal)
		{
			worldUpgradesTotal.totemDamageFactor *= (this.multiplier - 1.0) * world.universalBonus.milestoneBonusFactor + 1.0;
			if (world.totem != null)
			{
				world.totem.Refresh();
			}
		}

		public override string GetDescription(World world)
		{
			return string.Format(LM.Get("CHALLENGE_UPGRADE_DAMAGE_TOTEM"), GameMath.GetPercentString((this.multiplier - 1.0) * world.universalBonus.milestoneBonusFactor, true));
		}

		public double multiplier;
	}
}
