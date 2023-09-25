using System;
using System.Collections.Generic;

namespace Simulation
{
	public abstract class SkillPassiveDataBase : SkillTreeNode
	{
		public override bool IsActive()
		{
			return false;
		}

		public override bool IsPassive()
		{
			return true;
		}

		public override bool IsEnhancer()
		{
			return false;
		}

		public abstract void SetLevel(SkillPassiveData data, int level, List<SkillEnhancer> skillEnhancers);
	}
}
