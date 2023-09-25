using System;

namespace Simulation
{
	public class BuffDataHealAllyOnDamage : BuffData
	{
		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			double num = damage.amount * this.healRatio;
			Unit by = buff.GetBy();
			UnitHealthy unitHealthy = null;
			foreach (UnitHealthy unitHealthy2 in by.GetAllies())
			{
				if (unitHealthy2 != by)
				{
					if (unitHealthy2.IsAlive())
					{
						if (unitHealthy == null || unitHealthy2.GetHealthRatio() < unitHealthy.GetHealthRatio())
						{
							unitHealthy = unitHealthy2;
						}
					}
				}
			}
			if (unitHealthy != null)
			{
				double num2 = num / unitHealthy.GetHealthMax();
				unitHealthy.Heal(num2);
			}
		}

		public double healRatio;
	}
}
