using System;

namespace Simulation
{
	public class GearData
	{
		public string GetId()
		{
			string str = "gear.";
			string id = this.belongsTo.id;
			string str2 = ".";
			int num = (int)this.universalBonusType;
			return str + id + str2 + num.ToString();
		}

		public const int NUM_LEVELS = 6;

		public const float UNLOCKED_HERO_MISSING_GEAR_WEIGHT = 45f;

		public const float UNLOCKED_HERO_BOUGHT_GEAR_WEIGHT = 17f;

		public const float ALMOST_UNLOCKED_HERO_MISSING_GEAR_WEIGHT = 20f;

		public const float ALMOST_UNLOCKED_HERO_BOUGHT_GEAR_WEIGHT = 10f;

		public const float LOCKED_HERO_MISSING_GEAR_WEIGHT = 3f;

		public const float LOCKED_HERO_BOUGHT_GEAR_WEIGHT = 1f;

		public const int MAX_STAGES_TO_ALMOST_UNLOCKED_HERO = 50;

		public const int MAX_TC_BEATEN_TO_ALMOST_UNLOCKED_HERO = 3;

		public GearData.UniversalBonusType universalBonusType;

		public HeroDataBase belongsTo;

		public SkillTreeNode skillToLevelUp;

		public string nameKey;

		public enum UniversalBonusType
		{
			GOLD,
			HEALTH,
			DAMAGE
		}
	}
}
