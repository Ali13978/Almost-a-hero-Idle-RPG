using System;

namespace Simulation
{
	public class SkillEventDamageSelf : SkillEvent
	{
		public override void Apply(Unit by)
		{
			if (!(by is UnitHealthy))
			{
				return;
			}
			UnitHealthy unitHealthy = by as UnitHealthy;
			unitHealthy.TakeDamage(new Damage(this.damagePer * unitHealthy.GetHealthMax(), false, false, false, false)
			{
				isPure = true,
				isExact = true,
				canNotBeDodged = true,
				ignoreReduction = true,
				ignoreShield = true
			}, null, 0.01);
		}

		public override void Cancel(Unit by, float timePassedSinceActivation)
		{
		}

		public double damagePer;

		public DamageType damageType;
	}
}
