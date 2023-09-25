using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillPassiveData : SkillData
	{
		public SkillPassiveData(SkillPassiveDataBase dataBase, List<SkillEnhancer> skillEnhancers, int level = 0)
		{
			this.dataBase = dataBase;
			this.level = level;
			dataBase.SetLevel(this, level, skillEnhancers);
		}

		public override void SetLevel(int level, List<SkillEnhancer> skillEnhancers)
		{
			this.level = level;
			this.dataBase.SetLevel(this, level, skillEnhancers);
		}

		public override bool IsActive()
		{
			return false;
		}

		protected override SkillTreeNode GetSkillDataBase()
		{
			return this.dataBase;
		}

		public SkillPassiveDataBase dataBase;

		public BuffData passiveBuff;
	}
}
