using System;

namespace Simulation
{
	public class BuffDataEasyTargets : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			foreach (UnitHealthy unitHealthy in buff.GetBy().GetOpponents())
			{
				bool flag = unitHealthy.HasBuffWithId(341);
				if (unitHealthy.statCache.missChance > 0f && !flag)
				{
					unitHealthy.AddBuff(new BuffDataDamageTakenIncreased
					{
						damageFactor = this.damageFactor,
						isPermenant = false,
						isStackable = false,
						dur = 30f,
						id = 341
					}, 0, false);
				}
				else if (unitHealthy.statCache.missChance <= 0f && flag)
				{
					unitHealthy.RemoveBuff(341);
				}
			}
		}

		public double damageFactor;
	}
}
