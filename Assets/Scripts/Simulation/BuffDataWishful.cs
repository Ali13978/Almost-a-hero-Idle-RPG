using System;

namespace Simulation
{
	public class BuffDataWishful : BuffData
	{
		public BuffDataWishful(float durationRecharge)
		{
			this.durationRecharge = durationRecharge;
			this.id = 195;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.totemEarthTapRecharge += this.durationRecharge;
		}

		private float durationRecharge;
	}
}
