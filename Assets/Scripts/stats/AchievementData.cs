using System;

namespace stats
{
	public class AchievementData
	{
		public AchievementData(int numLevel, string[] ids, int[] targetProgesses, double[] rewardAmounts)
		{
			this.numLevel = numLevel;
			this.ids = ids;
			this.targetProgesses = targetProgesses;
			this.rewardAmounts = rewardAmounts;
		}

		public AchievementData(int levelCount)
		{
			this.numLevel = levelCount;
			this.ids = new string[levelCount];
			this.targetProgesses = new int[levelCount];
			this.rewardAmounts = new double[levelCount];
		}

		public int numLevel;

		public string[] ids;

		public int[] targetProgesses;

		public double[] rewardAmounts;
	}
}
