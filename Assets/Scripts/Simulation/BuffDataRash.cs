using System;

namespace Simulation
{
	public class BuffDataRash : BuffData
	{
		public BuffDataRash(float delay, int chargeRequired)
		{
			this.chargeRequired = chargeRequired;
			this.delay = delay;
			this.timer = delay;
			this.id = 144;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.totemChargeReqAdd += this.chargeRequired;
			this.timer -= dt;
			if (this.timer <= 0f)
			{
				this.timer = this.delay;
				totEffect.totemHasShotAuto = true;
			}
		}

		private float delay;

		private int chargeRequired;

		private float timer;
	}
}
