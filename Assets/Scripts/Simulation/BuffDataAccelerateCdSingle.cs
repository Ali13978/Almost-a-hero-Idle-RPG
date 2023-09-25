using System;

namespace Simulation
{
	public class BuffDataAccelerateCdSingle : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.skillCoolFactor *= this.skillCooldownFactor;
		}

		public float skillCooldownFactor;
	}
}
