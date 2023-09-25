using System;

namespace Simulation
{
	public class BuffDataCritChance : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.critChanceAdd += this.critChanceAdd;
		}

		public float critChanceAdd;
	}
}
