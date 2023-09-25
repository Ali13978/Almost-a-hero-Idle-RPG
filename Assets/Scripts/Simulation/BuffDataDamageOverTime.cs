using System;

namespace Simulation
{
	public class BuffDataDamageOverTime : BuffData
	{
		public BuffDataDamageOverTime(double damage, float duration, Unit damager)
		{
			this.dur = duration;
			this.tickCount = GameMath.FloorToInt(duration / 0.3f);
			this.damagePerTick = damage / (double)this.tickCount;
			this.damager = damager;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			if (this.tickCounter >= this.tickCount)
			{
				return;
			}
			this.genericTimer += dt;
			if (this.genericTimer >= 0.3f)
			{
				this.tickCounter++;
				this.genericTimer = 0f;
				UnitHealthy unitHealthy = buff.GetBy() as UnitHealthy;
				Damage damage = new Damage(this.damagePerTick, false, false, false, false);
				if (GameMath.GetProbabilityOutcome(this.damager.GetCritChance(), GameMath.RandType.NoSeed))
				{
					damage.amount *= this.damager.GetCritFactor();
					damage.isCrit = true;
				}
				unitHealthy.TakeDamage(damage, this.damager, 0.0);
			}
		}

		private const float Tick = 0.3f;

		private int tickCount;

		private int tickCounter;

		private double damagePerTick;

		private Unit damager;
	}
}
