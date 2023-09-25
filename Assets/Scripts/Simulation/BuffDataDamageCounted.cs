using System;

namespace Simulation
{
	public class BuffDataDamageCounted : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.damageAddFactor += this.damageAdd;
		}

		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			buff.DecreaseLifeCounter();
		}

		public double damageAdd;
	}
}
