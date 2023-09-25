using System;

namespace Simulation
{
	public class BuffDataAttackSpeedFactor : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.attackSpeedFactor *= this.attackSpeedFactor;
		}

		public float attackSpeedFactor;
	}
}
