using System;
using System.Collections.Generic;

namespace Simulation
{
	public class BuffDataReduceReviveOnDeath : BuffData
	{
		public override void OnDeathSelf(Buff buff)
		{
			Hero hero = buff.GetBy() as Hero;
			IEnumerable<UnitHealthy> allies = hero.GetAllies();
			foreach (UnitHealthy unitHealthy in allies)
			{
				if (unitHealthy != hero && !unitHealthy.IsAlive())
				{
					unitHealthy.inStateTimeCounter += this.reduction;
					unitHealthy.AddVisualBuff(3f, 32768);
				}
			}
		}

		public float reduction;
	}
}
