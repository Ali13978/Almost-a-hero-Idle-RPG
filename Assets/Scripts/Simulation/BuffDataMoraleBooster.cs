using System;

namespace Simulation
{
	public class BuffDataMoraleBooster : BuffData
	{
		public BuffDataMoraleBooster(double damageFactor)
		{
			this.damageFactor = damageFactor;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			foreach (UnitHealthy unitHealthy in buff.GetBy().GetOpponents())
			{
				if (unitHealthy.HasBuffStun())
				{
					unitHealthy.AddBuff(new BuffDataDamageTakenIncreased
					{
						damageFactor = this.damageFactor,
						isPermenant = false,
						isStackable = false,
						dur = 1f,
						id = 361
					}, 0, false);
				}
			}
		}

		private double damageFactor;
	}
}
