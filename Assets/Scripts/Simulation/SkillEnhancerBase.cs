using System;

namespace Simulation
{
	public class SkillEnhancerBase : SkillTreeNode
	{
		public override bool IsActive()
		{
			return false;
		}

		public override bool IsPassive()
		{
			return false;
		}

		public override bool IsEnhancer()
		{
			return true;
		}
	}
}
