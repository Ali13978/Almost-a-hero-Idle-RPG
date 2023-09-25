using System;

namespace Simulation
{
	public class BuffDataRage : BuffData
	{
		public BuffDataRage(float attackSpeedAdd, float ragePeriod, float eventPeriod)
		{
			this.attackSpeedAdd = attackSpeedAdd;
			this.eventPeriod = eventPeriod;
			this.ragePeriod = ragePeriod;
			this.genericTimer = eventPeriod;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			this.genericTimer += dt;
			if (this.genericTimer >= this.eventPeriod)
			{
				this.genericTimer = 0f;
				BuffDataAttackSpeed buffDataAttackSpeed = new BuffDataAttackSpeed();
				buffDataAttackSpeed.id = 220;
				buffDataAttackSpeed.isStackable = true;
				buffDataAttackSpeed.attackSpeedAdd = this.attackSpeedAdd;
				buffDataAttackSpeed.dur = this.ragePeriod;
				buffDataAttackSpeed.visuals |= 4096;
				buff.GetBy().AddBuff(buffDataAttackSpeed, 0, false);
			}
		}

		private float attackSpeedAdd;

		private float ragePeriod;

		private float eventPeriod;
	}
}
