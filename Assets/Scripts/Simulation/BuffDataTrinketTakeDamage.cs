using System;

namespace Simulation
{
	public class BuffDataTrinketTakeDamage : BuffData
	{
		public override void OnTakenDamage(Buff buff, Unit attacker, Damage damage)
		{
			Hero hero = buff.GetBy() as Hero;
			Trinket trinket = hero.trinket;
			trinket.req.IncreaseProgress(1);
		}
	}
}
