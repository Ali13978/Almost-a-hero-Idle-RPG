using System;

namespace Simulation
{
	public class FutureDamage
	{
		public FutureDamage(Unit damager, UnitHealthy damaged, Damage damage, float time)
		{
			this.damager = damager;
			this.damaged = damaged;
			this.damage = damage;
			this.timeLeft = time;
		}

		public Unit damager;

		public UnitHealthy damaged;

		public Damage damage;

		public float timeLeft;
	}
}
