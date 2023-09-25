using System;

namespace Simulation
{
	public class BuffDataSpree : BuffData
	{
		public BuffDataSpree(double damageAdd, float cooldown)
		{
			this.damageAdd = damageAdd;
			this.cooldown = cooldown;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			if (this.genericFlag)
			{
				return;
			}
			this.genericTimer += dt;
			if (this.genericTimer >= this.cooldown)
			{
				this.genericTimer = 0f;
				this.genericFlag = true;
			}
		}

		public override void OnKilled(Buff buff, UnitHealthy killed)
		{
			if (this.genericFlag)
			{
				UnitHealthy unitHealthy = buff.GetBy() as UnitHealthy;
				if (!unitHealthy.IsAlly(killed))
				{
					this.genericFlag = false;
					BuffDataDamageCounted buffDataDamageCounted = new BuffDataDamageCounted();
					buffDataDamageCounted.id = 47;
					buffDataDamageCounted.damageAdd = this.damageAdd;
					buffDataDamageCounted.lifeCounter = 1;
					buffDataDamageCounted.dur = float.PositiveInfinity;
					buffDataDamageCounted.visuals = 4096;
					buff.GetBy().AddBuff(buffDataDamageCounted, 0, true);
				}
			}
		}

		private double damageAdd;

		private float cooldown;
	}
}
