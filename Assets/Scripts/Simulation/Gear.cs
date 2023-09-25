using System;

namespace Simulation
{
	public class Gear
	{
		public Gear(GearData data, int level = 0)
		{
			this.data = data;
			this.level = level;
		}

		public Gear GetCopy()
		{
			return new Gear(this.data, this.level);
		}

		public string UniversalBonusString()
		{
			if (this.data.universalBonusType == GearData.UniversalBonusType.GOLD)
			{
				return LM.Get("GEAR_BONUS_GOLD");
			}
			if (this.data.universalBonusType == GearData.UniversalBonusType.DAMAGE)
			{
				return LM.Get("GEAR_BONUS_DAMAGE");
			}
			if (this.data.universalBonusType == GearData.UniversalBonusType.HEALTH)
			{
				return LM.Get("GEAR_BONUS_HEALTH");
			}
			throw new NotImplementedException();
		}

		public string SkillBonusString()
		{
			return LM.Get(this.data.skillToLevelUp.nameKey) + LM.Get("S_COLON");
		}

		public int GetSkillBonusPercent(int addLevel = 0)
		{
			return this.level + 1 + addLevel;
		}

		public string SkillBonusPercentString(int addLevel = 0)
		{
			return "+" + this.GetSkillBonusPercent(addLevel).ToString();
		}

		public double GetUniversalBonusPercent(UniversalTotalBonus universalTotalBonus, int addLevel = 0)
		{
			return Simulator.GEAR_EFFICIENCY[this.level + addLevel];
		}

		public string UniversalBonusPercentString(UniversalTotalBonus universalTotalBonus, int addLevel = 0)
		{
			double universalBonusPercent = this.GetUniversalBonusPercent(universalTotalBonus, addLevel);
			if (universalBonusPercent < 0.0)
			{
				return string.Empty;
			}
			return "+" + GameMath.GetPercentString(this.GetUniversalBonusPercent(universalTotalBonus, addLevel) * 0.01, false);
		}

		public bool IsMaxLevel()
		{
			return this.level >= 5;
		}

		public string GetId()
		{
			return this.data.GetId();
		}

		public int GetSkillLevelIncAmount()
		{
			return this.level + 1;
		}

		public GearData data;

		public int level;
	}
}
