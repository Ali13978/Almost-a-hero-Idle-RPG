using System;

namespace Simulation
{
	public class BuffDataSmash : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.totemChargeReqAdd += this.chargeReqAdd;
		}

		public override void OnPreProjectile(Buff buff, Projectile projectile)
		{
			projectile.damageAreaR = 0.3f;
			projectile.damageArea = new Damage(projectile.damage.amount * this.areaDamageRatio, false, false, false, false);
		}

		private const float areaR = 0.3f;

		public double areaDamageRatio;

		public int chargeReqAdd;
	}
}
