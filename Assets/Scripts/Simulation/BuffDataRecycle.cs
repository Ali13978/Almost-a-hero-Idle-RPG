using System;

namespace Simulation
{
	public class BuffDataRecycle : BuffData
	{
		public override void ApplyWorldEffect(BuffTotalWorldEffect totEffect, Buff buff)
		{
			totEffect.goldFactor += this.goldFactor;
		}

		public double goldFactor;
	}
}
