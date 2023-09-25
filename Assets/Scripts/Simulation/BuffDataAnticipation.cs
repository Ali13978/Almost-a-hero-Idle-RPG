using System;

namespace Simulation
{
	public class BuffDataAnticipation : BuffData
	{
		public BuffDataAnticipation(double critFactorAdd, float duration, float cooldown)
		{
			this.critFactorAdd = critFactorAdd;
			this.duration = duration;
			this.cooldown = cooldown;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			if (this.genericFlag)
			{
				return;
			}
			this.genericTimer += dt;
			if (this.genericTimer >= this.cooldown)
			{
				this.genericTimer = 0f;
				this.genericFlag = true;
			}
		}

		public override void OnCastSpellSelf(Buff buff, Skill skill)
		{
			if (this.genericFlag)
			{
				this.genericFlag = false;
				BuffDataCritFactor buffDataCritFactor = new BuffDataCritFactor();
				buffDataCritFactor.id = 31;
				buffDataCritFactor.visuals = 4096;
				buffDataCritFactor.critFactorAdd = this.critFactorAdd;
				buffDataCritFactor.dur = this.duration;
				buff.GetBy().AddBuff(buffDataCritFactor, 0, false);
			}
		}

		private double critFactorAdd;

		private float duration;

		private float cooldown;
	}
}
