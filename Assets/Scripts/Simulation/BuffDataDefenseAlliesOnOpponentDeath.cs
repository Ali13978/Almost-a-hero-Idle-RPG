using System;

namespace Simulation
{
	public class BuffDataDefenseAlliesOnOpponentDeath : BuffData
	{
		public override void OnOpponentDeath(Buff buff, UnitHealthy dead)
		{
			foreach (UnitHealthy unitHealthy in buff.GetBy().GetAllies())
			{
				if (unitHealthy.IsAlive() && unitHealthy.GetBuffsCountWithId(329) < this.maxBuffsCount)
				{
					unitHealthy.AddBuff(new BuffDataDefense
					{
						damageTakenFactor = this.damageTakenFactor,
						dur = this.damageReductionDuration,
						isStackable = true,
						id = 329,
						visuals = 512
					}, 0, false);
				}
			}
		}

		public double damageTakenFactor;

		public float damageReductionDuration;

		public int maxBuffsCount;
	}
}
