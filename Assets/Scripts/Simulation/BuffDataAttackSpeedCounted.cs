using System;

namespace Simulation
{
	public class BuffDataAttackSpeedCounted : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.attackSpeedAdd += this.attackSpeedFactor;
		}

		public override void OnAttackTargetChanged(Buff buff, UnitHealthy oldTarget, UnitHealthy newTarget)
		{
			buff.DecreaseLifeCounter();
		}

		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			if (target != null && !target.IsAlive())
			{
				buff.DecreaseLifeCounter();
			}
		}

		public float attackSpeedFactor;
	}
}
