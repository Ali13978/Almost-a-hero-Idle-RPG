using System;

namespace Simulation
{
	public class BuffDataOverload : BuffData
	{
		public BuffDataOverload(double damageBonus)
		{
			this.damageBonus = damageBonus;
			this.id = 138;
		}

		public override void OnAfterLightning(Buff buff, UnitHealthy target, Damage damage)
		{
			if (!target.IsAlive())
			{
				buff.IncreaseGenericCounter();
			}
		}

		public override void OnPreThunderbolt(Buff buff, UnitHealthy target, Damage damage, bool isSecondary)
		{
			if (isSecondary)
			{
				return;
			}
			double num = 1.0 + (double)buff.GetGenericCounter() * this.damageBonus;
			buff.ZeroGenericCounter();
			damage.amount *= num;
		}

		private double damageBonus;
	}
}
