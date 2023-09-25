using System;

namespace Simulation
{
	public class BuffDataMissChanceAdd : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.missChanceAdd += this.missChanceAdd;
		}

		public float missChanceAdd;
	}
}
