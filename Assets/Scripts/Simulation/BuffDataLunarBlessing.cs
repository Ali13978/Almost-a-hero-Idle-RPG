using System;

namespace Simulation
{
	public class BuffDataLunarBlessing : BuffData
	{
		public BuffDataLunarBlessing(float inc)
		{
			this.inc = inc;
			this.id = 121;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.totemIceManaMaxFactor += this.inc;
		}

		private float inc;
	}
}
