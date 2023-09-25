using System;

namespace Simulation
{
	public class BuffDataGoldChest : BuffData
	{
		public override void ApplyWorldEffect(BuffTotalWorldEffect totEffect, Buff buff)
		{
			totEffect.goldChestFactor += this.goldFactorAdd;
		}

		public double goldFactorAdd;
	}
}
