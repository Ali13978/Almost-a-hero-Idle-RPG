using System;

namespace Simulation
{
	public class BuffDataSkillReduceUpgradeCost : BuffData
	{
		public override void ApplyStats(BuffTotalUnitEffect totEffect, Buff buff, float dt)
		{
			totEffect.upgradeCostFactor *= 1.0 - this.reductionRatio;
		}

		public override void OnCastSpellSelf(Buff buff, Skill skill)
		{
			this.reductionRatio = GameMath.GetMinDouble(this.reductionRatio + this.reductionOnSkill, this.reductionMax);
		}

		public override void OnUpgradedSelf(Buff buff)
		{
			this.reductionRatio = 0.0;
		}

		public double reductionMax;

		public double reductionOnSkill;

		private double reductionRatio;
	}
}
