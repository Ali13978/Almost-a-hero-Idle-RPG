using System;

namespace stats
{
	[Serializable]
	public class Achievement
	{
		public Achievement(AchievementData data)
		{
			this.data = data;
		}

		public void OnProgress(int progress)
		{
			this.currentProgress = progress;
		}

		public int GetCurrentLevel()
		{
			int num = 0;
			for (int i = 0; i < this.data.numLevel; i++)
			{
				if (this.data.targetProgesses[i] <= this.currentProgress)
				{
					num++;
				}
			}
			return num;
		}

		public bool IsLevelAchieved(int level)
		{
			level--;
			return this.currentProgress >= this.data.targetProgesses[level];
		}

		public bool HasCollectedAll()
		{
			return this.lastCollectedLevel >= this.data.numLevel;
		}

		public bool HasUncollectedProgress()
		{
			return !this.HasCollectedAll() && this.GetCurrentLevel() > this.lastCollectedLevel;
		}

		public double GetNextRewardAmont()
		{
			return this.data.rewardAmounts[this.lastCollectedLevel];
		}

		public float GetCurrentProgress()
		{
			int num = this.GetCurrentLevel() + 1;
			if (num >= this.data.numLevel)
			{
				return 1f;
			}
			return (float)this.currentProgress / (float)this.data.targetProgesses[num];
		}

		public double GetAndCollectNextAvailable()
		{
			if (this.HasCollectedAll() || !this.HasUncollectedProgress())
			{
				return 0.0;
			}
			double nextRewardAmont = this.GetNextRewardAmont();
			this.lastCollectedLevel++;
			return nextRewardAmont;
		}

		public AchievementData data;

		public int lastCollectedLevel;

		public int currentProgress;
	}
}
