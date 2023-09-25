using System;
using System.Collections.Generic;

namespace Simulation
{
	public class SkillEnhancer : SkillData
	{
		public SkillEnhancer(SkillEnhancerBase enhancerBase, int level = 0)
		{
			this.enhancerBase = enhancerBase;
			this.level = level;
		}

		public override void SetLevel(int level, List<SkillEnhancer> skillEnhancers)
		{
			this.level = level;
		}

		protected override SkillTreeNode GetSkillDataBase()
		{
			return this.enhancerBase;
		}

		public SkillEnhancerBase enhancerBase;
	}
}
