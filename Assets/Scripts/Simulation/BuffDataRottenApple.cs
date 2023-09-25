using System;

namespace Simulation
{
	public class BuffDataRottenApple : BuffData
	{
		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			target.AddBuff(this.effect, 0, false);
		}

		public BuffDataDefense effect;
	}
}
