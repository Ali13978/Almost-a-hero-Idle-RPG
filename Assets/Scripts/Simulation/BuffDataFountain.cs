using System;

namespace Simulation
{
	public class BuffDataFountain : BuffData
	{
		public BuffDataFountain(float increase)
		{
			this.increase = increase;
		}

		public override void ApplyWorldEffect(BuffTotalWorldEffect totEffect, Buff buff)
		{
			totEffect.heroHealFactor *= (double)this.increase;
		}

		private float increase;
	}
}
