using System;

namespace Simulation
{
	public class BuffDataBittersweet : BuffData
	{
		public override void ApplyWorldEffect(BuffTotalWorldEffect totEffect, Buff buff)
		{
			totEffect.heroSkillCoolFactor *= this.skillCooldownFactor;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.skillCoolFactor /= this.skillCooldownFactor;
		}

		public float skillCooldownFactor;
	}
}
