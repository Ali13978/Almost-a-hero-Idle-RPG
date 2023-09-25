using System;

namespace Simulation
{
	public class BuffDataLightningShield : BuffData
	{
		public BuffDataLightningShield(double damageRatio, float duration, float cooldown)
		{
			this.damageRatio = damageRatio;
			this.duration = duration;
			this.cooldown = cooldown;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			this.genericTimer += dt;
			if (this.genericTimer >= this.cooldown)
			{
				this.genericTimer = 0f;
				BuffDataLightningShieldPunishment buffDataLightningShieldPunishment = new BuffDataLightningShieldPunishment(this.damageRatio);
				buffDataLightningShieldPunishment.id = 228;
				buffDataLightningShieldPunishment.dur = this.duration;
				buffDataLightningShieldPunishment.visuals |= 4096;
				buff.GetBy().AddBuff(buffDataLightningShieldPunishment, 0, false);
			}
		}

		private float cooldown;

		private float duration;

		private double damageRatio;
	}
}
