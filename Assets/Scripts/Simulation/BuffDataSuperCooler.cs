using System;

namespace Simulation
{
	public class BuffDataSuperCooler : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.totemCoolFactor += this.coolFactorAdd;
		}

		public float coolFactorAdd;
	}
}
