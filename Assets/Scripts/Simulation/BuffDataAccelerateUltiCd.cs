using System;

namespace Simulation
{
	public class BuffDataAccelerateUltiCd : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.ultiCoolFactor += this.skillCooldownFactor;
		}

		public float skillCooldownFactor;
	}
}
