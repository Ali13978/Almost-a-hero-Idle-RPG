using System;

namespace Simulation
{
	public class LevelSkipPairs
	{
		public int GetSkipCountForRecord(float record, int stage)
		{
			if (this.timeLimit >= record)
			{
				for (int i = 0; i < this.skipLimits.Length; i++)
				{
					if (stage < this.skipLimits[i])
					{
						return this.skipCounts[i];
					}
				}
			}
			return 0;
		}

		public int[] skipLimits;

		public int[] skipCounts;

		public float timeLimit;
	}
}
