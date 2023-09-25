using System;

namespace Simulation
{
	public class BuffDataOneShot : BuffData
	{
		public override void OnKilled(Buff buff, UnitHealthy killed)
		{
			buff.IncreaseGenericCounter();
		}

		public override void OnPreDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			if (buff.GetGenericCounter() < this.reqNumKill)
			{
				return;
			}
			if (target is Enemy)
			{
				Enemy enemy = (Enemy)target;
				if (!enemy.IsBoss())
				{
					damage.amount = enemy.GetHealthMax() * 1.5;
					damage.dontCountForDps = true;
					buff.ZeroGenericCounter();
				}
			}
		}

		public int reqNumKill;
	}
}
