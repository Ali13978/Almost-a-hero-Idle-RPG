using System;

namespace Simulation
{
	public class BuffDataStinger : BuffData
	{
		public BuffDataStinger(float delay)
		{
			this.delay = delay;
			this.timer = delay;
			this.id = 289;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			this.timer -= dt;
			if (this.timer <= 0f)
			{
				this.timer = this.delay;
				totEffect.totemHasShotAuto = true;
			}
		}

		private float delay;

		private float timer;
	}
}
