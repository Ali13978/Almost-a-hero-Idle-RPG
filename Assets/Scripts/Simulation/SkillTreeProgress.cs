using System;

namespace Simulation
{
	public class SkillTreeProgress
	{
		public SkillTreeProgress()
		{
		}

		public SkillTreeProgress(int branchLength0, int branchLength1)
		{
			this.Init(branchLength0, branchLength1);
		}

		public SkillTreeProgress(SkillTree skillTree)
		{
			int branchLength = skillTree.branches[0].Length;
			int branchLength2 = skillTree.branches[1].Length;
			this.Init(branchLength, branchLength2);
		}

		private void Init(int branchLength0, int branchLength1)
		{
			this.ulti = 0;
			this.branches = new int[][]
			{
				new int[branchLength0],
				new int[branchLength1]
			};
			for (int i = branchLength0 - 1; i >= 0; i--)
			{
				this.branches[0][i] = -1;
			}
			for (int j = branchLength1 - 1; j >= 0; j--)
			{
				this.branches[1][j] = -1;
			}
		}

		public void InitAsAdditional()
		{
			this.ulti = 0;
			for (int i = this.branches.Length - 1; i >= 0; i--)
			{
				int[] array = this.branches[i];
				for (int j = array.Length - 1; j >= 0; j--)
				{
					array[j] = 0;
				}
			}
		}

		public void SetAsTotal(SkillTreeProgress a, SkillTreeProgress b)
		{
			this.ulti = a.ulti + b.ulti;
			for (int i = this.branches.Length - 1; i >= 0; i--)
			{
				int[] array = this.branches[i];
				int[] array2 = a.branches[i];
				int[] array3 = b.branches[i];
				for (int j = array.Length - 1; j >= 0; j--)
				{
					array[j] = array2[j] + array3[j];
				}
			}
		}

		public int ulti;

		public int[][] branches;
	}
}
