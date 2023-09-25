using System;

namespace Simulation
{
	public class BuffDataImmunityAfterSelfDestruct : BuffData
	{
		public BuffDataImmunityAfterSelfDestruct(float immunityDuration, float immunityPeriod)
		{
			this.timer = immunityPeriod;
			this.immunityDuration = immunityDuration;
			this.immunityPeriod = immunityPeriod;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			this.timer += dt;
		}

		public override void OnCastSpellSelf(Buff buff, Skill skill)
		{
			base.OnCastSpellSelf(buff, skill);
			if (this.timer >= this.immunityPeriod && skill.data.dataBase is SkillDataBaseSelfDestruct)
			{
				this.timer = 0f;
				BuffDataInvulnerability buffDataInvulnerability = new BuffDataInvulnerability(this.immunityDuration);
				buffDataInvulnerability.visuals = 128;
				buffDataInvulnerability.id = 113;
				buff.GetBy().AddBuff(buffDataInvulnerability, 0, false);
			}
		}

		private float immunityDuration;

		private float immunityPeriod;

		private float timer;
	}
}
