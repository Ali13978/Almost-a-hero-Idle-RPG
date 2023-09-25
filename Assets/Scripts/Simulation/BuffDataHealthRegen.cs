using System;

namespace Simulation
{
	public class BuffDataHealthRegen : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			if (!((UnitHealthy)buff.GetBy()).IsAlive())
			{
				return;
			}
			totEffect.healthRegenAdd += this.healthRegenAdd;
		}

		public double healthRegenAdd;
	}
}
