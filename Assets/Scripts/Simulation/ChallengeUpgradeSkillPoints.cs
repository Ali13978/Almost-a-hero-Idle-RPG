using System;

namespace Simulation
{
	public class ChallengeUpgradeSkillPoints : ChallengeUpgrade
	{
		public ChallengeUpgradeSkillPoints(int amount)
		{
			this.amount = amount;
		}

		public override void Apply(World world, ChallengeUpgradesTotal worldUpgradesTotal)
		{
			worldUpgradesTotal.skillPointsAdd += this.amount;
			if (world.givenSkillPoints < worldUpgradesTotal.skillPointsAdd)
			{
				int num = worldUpgradesTotal.skillPointsAdd - world.givenSkillPoints;
				foreach (Hero hero in world.heroes)
				{
					hero.IncrementNumUnspentSkillPoints(num);
				}
				world.givenSkillPoints += num;
			}
		}

		public override string GetDescription(World world)
		{
			return string.Format(LM.Get("CHALLENGE_UPGRADE_SKILL_POINTS"), this.amount.ToString());
		}

		public int amount;
	}
}
