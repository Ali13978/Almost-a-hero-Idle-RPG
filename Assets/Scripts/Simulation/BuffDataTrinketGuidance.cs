using System;

namespace Simulation
{
	public class BuffDataTrinketGuidance : BuffData
	{
		public BuffDataTrinketGuidance(float damageConverted, float period, float eventPeriod)
		{
			this.damageConverted = damageConverted;
			this.eventPeriod = eventPeriod;
			this.period = period;
			this.genericTimer = eventPeriod;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			this.genericTimer += dt;
		}

		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			if (this.genericTimer >= this.eventPeriod)
			{
				this.genericTimer = 0f;
				BuffDataHealAllyOnDamage buffDataHealAllyOnDamage = new BuffDataHealAllyOnDamage();
				buffDataHealAllyOnDamage.healRatio = (double)this.damageConverted;
				buffDataHealAllyOnDamage.id = 227;
				buffDataHealAllyOnDamage.isStackable = true;
				buffDataHealAllyOnDamage.dur = this.period;
				buffDataHealAllyOnDamage.visuals |= 4096;
				buff.GetBy().AddBuff(buffDataHealAllyOnDamage, 0, false);
			}
		}

		private float damageConverted;

		private float period;

		private float eventPeriod;
	}
}
