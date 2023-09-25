using System;

namespace Simulation
{
	public class BuffDataTogetherWeStand : BuffData
	{
		public override void ApplyWorldEffect(BuffTotalWorldEffect totEffect, Buff buff)
		{
			Unit by = buff.GetBy();
			foreach (Unit unit in by.GetAllies())
			{
				if (unit is UnitHealthy)
				{
					UnitHealthy unitHealthy = (UnitHealthy)unit;
					if (!unitHealthy.IsAlive())
					{
						return;
					}
				}
			}
			totEffect.heroDamageTakenFactor *= this.damageTakenFactor;
		}

		public double damageTakenFactor;
	}
}
