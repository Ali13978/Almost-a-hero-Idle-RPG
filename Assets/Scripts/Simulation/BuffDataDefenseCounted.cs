using System;

namespace Simulation
{
	public class BuffDataDefenseCounted : BuffData
	{
		public BuffDataDefenseCounted(double damageTakenFactor, int lifeCounter)
		{
			this.damageTakenFactor = damageTakenFactor;
			this.lifeCounter = lifeCounter;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.damageTakenFactor *= this.damageTakenFactor;
		}

		public override void OnTakenDamage(Buff buff, Unit attacker, Damage damage)
		{
			buff.DecreaseLifeCounter();
		}

		public double damageTakenFactor;
	}
}
