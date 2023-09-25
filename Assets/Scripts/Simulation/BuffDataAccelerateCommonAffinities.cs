using System;
using System.Collections.Generic;

namespace Simulation
{
	public class BuffDataAccelerateCommonAffinities : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			World world = buff.GetWorld();
			Hero hero = buff.GetBy() as Hero;
			bool flag = world.autoTapTimeLeft > 0f;
			int skillCooldownIndex = hero.GetSkillCooldownIndex(typeof(SkillDataBaseCommonAffinities));
			if (world.noRingTabDur >= this.idleDur && !flag)
			{
				if (skillCooldownIndex != -1)
				{
					List<float> skillCooldowns = hero.GetSkillCooldowns();
					float num = this.lastTime - skillCooldowns[skillCooldownIndex];
					if (num > 0f)
					{
						List<float> list;
						int index;
						(list = skillCooldowns)[index = skillCooldownIndex] = list[index] - num * this.speedupPercentage;
					}
					this.lastTime = skillCooldowns[skillCooldownIndex];
				}
			}
			else if (skillCooldownIndex != -1)
			{
				List<float> skillCooldowns2 = hero.GetSkillCooldowns();
				this.lastTime = skillCooldowns2[skillCooldownIndex];
			}
		}

		public float idleDur;

		public float speedupPercentage;

		private float lastTime;
	}
}
