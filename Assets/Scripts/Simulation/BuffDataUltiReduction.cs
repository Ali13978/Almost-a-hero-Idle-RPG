using System;

namespace Simulation
{
	public class BuffDataUltiReduction : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			Hero hero = buff.GetBy() as Hero;
			float timeDecrement = hero.GetMainSkillCooldownMax() * this.reduction;
			hero.DecreaseUltiCooldown(timeDecrement);
		}

		public float reduction;
	}
}
