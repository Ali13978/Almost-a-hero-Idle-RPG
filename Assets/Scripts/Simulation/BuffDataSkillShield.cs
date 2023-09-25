using System;

namespace Simulation
{
	public class BuffDataSkillShield : BuffData
	{
		public BuffDataSkillShield(float skillCdAdd)
		{
			this.skillCdAdd = skillCdAdd;
		}

		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			UnitHealthy unitHealthy = buff.GetBy() as UnitHealthy;
			if (!unitHealthy.HasZeroShield())
			{
				totEffect.skillCoolFactor += this.skillCdAdd;
			}
		}

		private float skillCdAdd;
	}
}
