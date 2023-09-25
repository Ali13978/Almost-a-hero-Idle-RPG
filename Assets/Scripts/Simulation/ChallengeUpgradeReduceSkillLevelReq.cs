using System;

namespace Simulation
{
	public class ChallengeUpgradeReduceSkillLevelReq : ChallengeUpgrade
	{
		public ChallengeUpgradeReduceSkillLevelReq(int amount)
		{
			this.amount = amount;
		}

		public override void Apply(World world, ChallengeUpgradesTotal worldUpgradesTotal)
		{
			worldUpgradesTotal.heroLevelRequiredForSkillDecrease += this.amount;
		}

		public override string GetDescription(World world)
		{
			return string.Format(LM.Get("CHALLENGE_UPGRADE_SKILL_LEVEL"), this.amount.ToString());
		}

		public int amount;
	}
}
