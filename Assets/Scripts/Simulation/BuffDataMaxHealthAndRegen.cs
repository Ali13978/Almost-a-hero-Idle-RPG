using System;

namespace Simulation
{
	public class BuffDataMaxHealthAndRegen : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.healthRegenAdd += this.healthRegenAdd;
			totEffect.healthMaxFactor += this.healthBonus;
		}

		public double healthRegenAdd;

		public double healthBonus;
	}
}
