using System;
using System.Collections.Generic;

namespace Simulation
{
	public class BuffDataPoisonedDaggers : BuffData
	{
		public override void OnPreProjectile(Buff buff, Projectile projectile)
		{
			if (projectile.buffs == null)
			{
				projectile.buffs = new List<BuffData>();
			}
			projectile.buffs.Add(this.effect);
		}

		public BuffDataDamageAdd effect;
	}
}
