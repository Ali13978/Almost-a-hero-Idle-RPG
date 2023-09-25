using System;

namespace Simulation
{
	public class BuffDataIncreaseAttackWhenHealthLost : BuffData
	{
		public override void OnHealthLost(Buff buff, double ratio)
		{
			this.currentHealthLostRatio += ratio;
		}

		public override void OnPreDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			if (this.currentHealthLostRatio >= this.healthLostRatio)
			{
				this.currentHealthLostRatio -= this.healthLostRatio;
				damage.amount *= (double)this.damageFactor;
				Hero hero = buff.GetBy() as Hero;
				if (hero != null && hero.GetId() == "DRUID")
				{
					buff.GetWorld().dailyQuestRonImpulsiveSkillTriggeredCounter++;
				}
			}
		}

		public float damageFactor;

		public double healthLostRatio;

		private double currentHealthLostRatio;
	}
}
