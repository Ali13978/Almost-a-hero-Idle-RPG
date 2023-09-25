using System;

namespace Simulation
{
	public class ChallengeUpgradesTotal
	{
		public void Init()
		{
			this.numBought = 0;
			this.goldFactor = 1.0;
			this.healthFactor = 1.0;
			this.heroDamageFactor = 1.0;
			this.skillPointsAdd = 0;
			this.heroLevelRequiredForSkillDecrease = 0;
			this.totemDamageFactor = 1.0;
		}

		public int numBought;

		public double healthFactor;

		public double heroDamageFactor;

		public int skillPointsAdd;

		public int heroLevelRequiredForSkillDecrease;

		public double totemDamageFactor;

		public double goldFactor;
	}
}
