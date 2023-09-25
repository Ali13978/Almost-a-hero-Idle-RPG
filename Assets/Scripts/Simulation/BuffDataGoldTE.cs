using System;

namespace Simulation
{
	public class BuffDataGoldTE : BuffData
	{
		public override void ApplyWorldEffect(BuffTotalWorldEffect totEffect, Buff buff)
		{
			totEffect.goldFactorTE += this.goldAdd;
		}

		public double goldAdd;
	}
}
