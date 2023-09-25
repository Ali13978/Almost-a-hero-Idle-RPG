using System;

namespace Simulation
{
	public class BuffDataMarked : BuffData
	{
		public BuffDataMarked(double damageBonus, int lifeCounter)
		{
			this.damageBonus = damageBonus;
			this.lifeCounter = lifeCounter;
			this.visuals |= 32;
		}

		public override void OnPreTakeDamage(Buff buff, Unit attacker, Damage damage)
		{
			if (attacker is Totem)
			{
				buff.GetBy().statCache.damageTakenFactor *= this.damageBonus;
			}
		}

		public override void OnTakenDamage(Buff buff, Unit attacker, Damage damage)
		{
			if (attacker is Totem)
			{
				buff.GetBy().statCache.damageTakenFactor /= this.damageBonus;
				buff.DecreaseLifeCounter();
				buff.GetWorld().OnTamHitMarkedTargets();
			}
		}

		private double damageBonus;
	}
}
