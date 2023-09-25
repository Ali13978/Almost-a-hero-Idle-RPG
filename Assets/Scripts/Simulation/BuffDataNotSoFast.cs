using System;

namespace Simulation
{
	public class BuffDataNotSoFast : BuffData
	{
		public override void ApplyWorldEffect(BuffTotalWorldEffect totEffect, Buff buff)
		{
			totEffect.goldFactor += this.amount;
		}

		public double amount;
	}
}
