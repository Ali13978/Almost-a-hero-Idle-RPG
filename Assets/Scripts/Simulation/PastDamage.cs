using System;

namespace Simulation
{
	public class PastDamage
	{
		public PastDamage(Damage damage)
		{
			this.damage = damage;
			this.time = 0f;
		}

		public const float MAX_TIME = 0.9f;

		public Damage damage;

		public float time;
	}
}
