using System;

namespace Simulation
{
	public class BuffDataCityThief : BuffData
	{
		public override void ApplyWorldEffect(BuffTotalWorldEffect totEffect, Buff buff)
		{
			totEffect.goldFactor += this.goldAdd;
		}

		public double goldAdd;
	}
}
