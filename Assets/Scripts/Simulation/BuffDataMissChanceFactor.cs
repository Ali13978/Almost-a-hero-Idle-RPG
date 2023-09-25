using System;

namespace Simulation
{
	public class BuffDataMissChanceFactor : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.missChanceFactor += this.missChanceFactorAdd;
		}

		public float missChanceFactorAdd;
	}
}
