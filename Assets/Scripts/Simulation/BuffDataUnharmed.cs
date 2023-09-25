using System;

namespace Simulation
{
	public class BuffDataUnharmed : BuffData
	{
		public BuffDataUnharmed(double damageRatio, float cooldown)
		{
			this.damageRatio = damageRatio;
			this.cooldown = cooldown;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			Hero hero = buff.GetBy() as Hero;
			if (hero == null)
			{
				return;
			}
			if (!hero.IsAlive() || buff.GetBy().HasBuffWithId(246))
			{
				this.genericTimer = 0f;
			}
			this.genericTimer += dt;
			if (this.genericTimer >= this.cooldown)
			{
				this.genericTimer = 0f;
				this.visuals = 4096;
				BuffDataDamageCounted buffDataDamageCounted = new BuffDataDamageCounted();
				buffDataDamageCounted.id = 246;
				buffDataDamageCounted.dur = float.PositiveInfinity;
				buffDataDamageCounted.damageAdd = this.damageRatio;
				buffDataDamageCounted.lifeCounter = 1;
				buff.GetBy().AddBuff(buffDataDamageCounted, 0, true);
			}
		}

		public override void OnTakenDamage(Buff buff, Unit attacker, Damage damage)
		{
			this.genericTimer = 0f;
		}

		public override void OnPostDamage(Buff buff, UnitHealthy target, Damage damage)
		{
			this.visuals = 0;
		}

		private double damageRatio;

		private float cooldown;
	}
}
