using System;

namespace Simulation
{
	public class BuffDataTrinketAttack : BuffData
	{
		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			Hero hero = buff.GetBy() as Hero;
			Trinket trinket = hero.trinket;
			trinket.req.IncreaseProgress(1);
		}
	}
}
