using System;

namespace Simulation
{
	public class BuffDataVillageGirl : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.heatMaxFactor += this.heatMaxAdd;
			totEffect.heatOvercoolFactor += this.heatMaxAdd;
		}

		public float heatMaxAdd;
	}
}
