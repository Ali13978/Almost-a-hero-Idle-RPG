using System;

namespace Simulation
{
	public class BuffDataIceBlast : BuffData
	{
		public BuffDataIceBlast(float speedFactor)
		{
			this.speedFactor = speedFactor;
			this.id = 105;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.totemIceManaGatherSpeedFactor += this.speedFactor;
			totEffect.totemIceManaUseSpeedFactor += this.speedFactor;
		}

		private float speedFactor;
	}
}
