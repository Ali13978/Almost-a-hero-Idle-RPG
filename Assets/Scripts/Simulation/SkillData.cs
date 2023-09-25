using System;
using System.Collections.Generic;

namespace Simulation
{
	public abstract class SkillData
	{
		public SkillData()
		{
			this.level = 0;
		}

		public abstract void SetLevel(int level, List<SkillEnhancer> skillEnhancers);

		public virtual bool IsActive()
		{
			return this.GetSkillDataBase().IsActive();
		}

		public virtual bool IsPassive()
		{
			return this.GetSkillDataBase().IsPassive();
		}

		public virtual bool IsEnhancer()
		{
			return this.GetSkillDataBase().IsEnhancer();
		}

		protected abstract SkillTreeNode GetSkillDataBase();

		public string GetDesc()
		{
			return this.GetSkillDataBase().GetDesc(this);
		}

		public string GetDescFull()
		{
			return this.GetSkillDataBase().GetDescFull(this);
		}

		public int level;
	}
}
