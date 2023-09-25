using System;
using System.Collections.Generic;

namespace Simulation
{
	public class BuffDataPaticipationThrophy : BuffData
	{
		public BuffDataPaticipationThrophy(double damageFactorAdd)
		{
			this.damageFactorAdd = damageFactorAdd;
		}

		public override void OnPreProjectile(Buff buff, Projectile projectile)
		{
			if (projectile.type == Projectile.Type.BABU_TEA_CUP)
			{
				return;
			}
			if (projectile.buffs == null)
			{
				projectile.buffs = new List<BuffData>();
			}
			Unit by = buff.GetBy();
			BuffDataDamageOverTime item = new BuffDataDamageOverTime(by.GetDpsTeam() * this.damageFactorAdd, this.duration, by);
			projectile.buffs.Add(item);
		}

		public double damageFactorAdd;

		public float duration;
	}
}
