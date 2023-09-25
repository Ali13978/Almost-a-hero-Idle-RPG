using System;
using System.Collections.Generic;

namespace Simulation
{
	public class BuffDataEnchant : BuffData
	{
		public override void OnPreProjectile(Buff buff, Projectile projectile)
		{
			if (projectile.buffs == null)
			{
				projectile.buffs = new List<BuffData>();
			}
			projectile.buffs.Add(this.effect);
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.totemChargeReqAdd += this.chargeReqAdd;
		}

		public BuffDataDefense effect;

		public int chargeReqAdd;
	}
}
