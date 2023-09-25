using System;

namespace Simulation
{
	public class BuffDataDropGold : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.dropGoldFactor += this.dropGoldFactorAdd;
		}

		public double dropGoldFactorAdd;
	}
}
