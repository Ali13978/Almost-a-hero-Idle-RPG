using System;

namespace Simulation
{
	public class BuffDataTrinketCritAttack : BuffData
	{
		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			Hero hero = buff.GetBy() as Hero;
			Trinket trinket = hero.trinket;
			if (damage.isCrit)
			{
				trinket.req.IncreaseProgress(1);
			}
		}
	}
}
