using System;

namespace Simulation
{
	public class BuffDataFury : BuffData
	{
		public BuffDataFury(float decrease, float duration, float cooldown)
		{
			this.decrease = decrease;
			this.duration = duration;
			this.cooldown = cooldown;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			this.genericTimer += dt;
			if (this.genericTimer >= this.cooldown)
			{
				this.genericTimer = 0f;
				BuffDataFurySkill buffDataFurySkill = new BuffDataFurySkill(this.decrease);
				buffDataFurySkill.id = 230;
				buffDataFurySkill.visuals = 4096;
				buffDataFurySkill.dur = this.duration;
				buff.GetBy().AddBuff(buffDataFurySkill, 0, true);
			}
		}

		private float decrease;

		private float duration;

		private float cooldown;
	}
}
