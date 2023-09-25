using System;

namespace Simulation
{
	public class BuffDataAcrobacy : BuffData
	{
		public BuffDataAcrobacy(float chance, float duration, float cooldown)
		{
			this.chance = chance;
			this.duration = duration;
			this.cooldown = cooldown;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			this.genericTimer += dt;
			if (this.genericTimer >= this.cooldown)
			{
				this.genericTimer = 0f;
				BuffDataDodge buffDataDodge = new BuffDataDodge();
				buffDataDodge.id = 233;
				buffDataDodge.visuals = 4096;
				buffDataDodge.dodgeChanceAdd = this.chance;
				buffDataDodge.dur = this.duration;
				buff.GetBy().AddBuff(buffDataDodge, 0, true);
			}
		}

		private float chance;

		private float duration;

		private float cooldown;
	}
}
