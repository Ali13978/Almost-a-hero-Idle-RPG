using System;

namespace Simulation
{
	public class BuffDataTargetAllCounted : BuffData
	{
		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			buff.DecreaseLifeCounter();
		}
	}
}
