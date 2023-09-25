using System;

namespace Simulation
{
	public class BuffDataBleak : BuffData
	{
		public BuffDataBleak(float speedFactor)
		{
			this.speedFactor = speedFactor;
			this.id = 17;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			if ((buff.GetBy() as TotemIce).isHoldingWhileRaining)
			{
				totEffect.totemIceManaSpendSpeedFactor -= this.speedFactor;
			}
		}

		private float speedFactor;
	}
}
