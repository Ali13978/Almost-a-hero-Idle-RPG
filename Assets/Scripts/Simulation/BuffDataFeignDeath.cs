using System;

namespace Simulation
{
	public class BuffDataFeignDeath : BuffData
	{
		public BuffDataFeignDeath(double healthRatio, float immunityDuration, float immunityOnceEveryPeriod)
		{
			this.healthRatio = healthRatio;
			this.immunityDuration = immunityDuration;
			this.immunityOnceEveryPeriod = immunityOnceEveryPeriod;
			this.immunityOnceEveryTimer = 0f;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			if (this.immunityOnceEveryTimer > 0f)
			{
				this.immunityOnceEveryTimer -= dt;
			}
		}

		public override void OnPreTakeDamage(Buff buff, Unit attacker, Damage damage)
		{
			if (this.immunityOnceEveryTimer <= 0f)
			{
				UnitHealthy unitHealthy = (UnitHealthy)buff.GetBy();
				double num = (unitHealthy.GetHealth() - damage.amount) / unitHealthy.GetHealthMax();
				if (unitHealthy.IsAlive() && num <= this.healthRatio)
				{
					damage.amount = 0.0;
					this.immunityOnceEveryTimer = this.immunityOnceEveryPeriod;
					BuffDataInvulnerability buffDataInvulnerability = new BuffDataInvulnerability();
					buffDataInvulnerability.id = 112;
					buffDataInvulnerability.visuals |= 128;
					buffDataInvulnerability.dur = this.immunityDuration;
					buff.GetBy().AddBuff(buffDataInvulnerability, 0, true);
				}
			}
		}

		private double healthRatio;

		private float immunityDuration;

		private float immunityOnceEveryPeriod;

		private float immunityOnceEveryTimer;
	}
}
