using System;

namespace Simulation
{
	public class BuffDataTracker : BuffData
	{
		public override void ApplyWorldEffect(BuffTotalWorldEffect totEffect, Buff buff)
		{
			totEffect.chestChanceAdd += this.chestChanceAdd;
		}

		public float chestChanceAdd;
	}
}
