using System;

namespace Simulation
{
	public abstract class SkillTreeNode
	{
		public bool HasMaximumLevelCap()
		{
			return this.maxLevel > 0;
		}

		public bool HasReachedMaxLevel(int level)
		{
			return this.HasMaximumLevelCap() && level >= this.maxLevel;
		}

		public virtual string GetDescZero()
		{
			return " $UNFILLED$ ";
		}

		public virtual string GetDesc(SkillData data)
		{
			return " $UNFILLED$ ";
		}

		public virtual string GetDescFull(SkillData data)
		{
			return " $UNFILLED$ ";
		}

		public abstract bool IsActive();

		public abstract bool IsPassive();

		public abstract bool IsEnhancer();

		public string nameKey;

		public string descKey;

		public string desc0Key;

		public string descFullKey;

		public int requiredHeroLevel;

		public int maxLevel;
	}
}
