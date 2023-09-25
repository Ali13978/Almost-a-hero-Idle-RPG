using System;

namespace Simulation
{
	public class BuffDataNova : BuffData
	{
		public BuffDataNova(float slowDown, float area)
		{
			this.slowDown = slowDown;
			this.area = area;
			this.id = 136;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.totemIceManaUseSpeedFactor -= this.slowDown;
			totEffect.damageAreaRFactor += this.area;
		}

		private float slowDown;

		private float area;
	}
}
