using System;

namespace Simulation
{
	public class BuffDataAlliesDamageChest : BuffData
	{
		public override void OnPreDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			Unit by = buff.GetBy();
			foreach (Unit unit in by.GetAllies())
			{
				if (unit is Hero)
				{
					Hero hero = (Hero)unit;
					hero.AddBuff(this.effect, 0, false);
				}
			}
		}

		public BuffDataDamageChest effect;
	}
}
