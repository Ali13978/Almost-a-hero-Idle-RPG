using System;

namespace Simulation
{
	public class BuffDataBandage : BuffData
	{
		public BuffDataBandage(float healPeriod, double healRatio)
		{
			this.healPeriod = healPeriod;
			this.healRatio = healRatio;
			this.timer = 0f;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			if (this.timerVisual > 0f)
			{
				this.visuals = 4096;
				this.timerVisual -= dt;
			}
			else
			{
				this.visuals = 0;
			}
			this.timer += dt;
			if (this.timer >= this.healPeriod)
			{
				this.timer = 0f;
				this.HealMostDamagedAlly(buff);
				this.timerVisual = 1f;
			}
		}

		private void HealMostDamagedAlly(Buff buff)
		{
			Unit by = buff.GetBy();
			UnitHealthy unitHealthy = null;
			foreach (UnitHealthy unitHealthy2 in by.GetAllies())
			{
				if (unitHealthy2.IsAlive())
				{
					if (unitHealthy == null || unitHealthy2.GetHealthRatio() < unitHealthy.GetHealthRatio())
					{
						unitHealthy = unitHealthy2;
					}
				}
			}
			if (unitHealthy != null)
			{
				BuffDataHealthRegen buffDataHealthRegen = new BuffDataHealthRegen();
				buffDataHealthRegen.id = 98;
				buffDataHealthRegen.isStackable = true;
				buffDataHealthRegen.dur = 1f;
				buffDataHealthRegen.healthRegenAdd = this.healRatio / 1.0;
				buffDataHealthRegen.visuals |= 64;
				unitHealthy.AddBuff(buffDataHealthRegen, 0, true);
			}
		}

		private float healPeriod;

		private double healRatio;

		private float timer;

		private float timerVisual;
	}
}
