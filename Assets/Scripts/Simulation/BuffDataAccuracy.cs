using System;

namespace Simulation
{
	public class BuffDataAccuracy : BuffData
	{
		public override void OnMissed(Buff buff, UnitHealthy target, Damage damage)
		{
			buff.IncreaseGenericCounter();
		}

		public override void OnPreDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			if (!damage.isDodged && !damage.isMissed)
			{
				double num = 1.0 + (double)buff.GetGenericCounter() * this.damageFactorAdd;
				damage.amount *= num;
				buff.ZeroGenericCounter();
			}
		}

		public double damageFactorAdd;
	}
}
