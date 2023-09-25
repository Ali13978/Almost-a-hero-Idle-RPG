using System;

namespace Simulation
{
	public class SkillTree
	{
		public SkillTree(SkillTreeNode ulti, SkillTreeNode[] branch1, SkillTreeNode[] branch2)
		{
			this.ulti = ulti;
			this.branches = new SkillTreeNode[][]
			{
				branch1,
				branch2
			};
		}

		public SkillTreeNode ulti;

		public SkillTreeNode[][] branches;
	}
}
