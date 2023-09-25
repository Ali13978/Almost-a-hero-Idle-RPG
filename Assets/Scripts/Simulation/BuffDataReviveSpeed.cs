using System;

namespace Simulation
{
	public class BuffDataReviveSpeed : BuffData
	{
		public override bool DontRemoveOnDeath()
		{
			return true;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.reviveSpeedFactor += this.reviveSpeedFactorAdd;
		}

		public float reviveSpeedFactorAdd;
	}
}
