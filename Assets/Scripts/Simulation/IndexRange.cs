using System;

namespace Simulation
{
	public class IndexRange
	{
		public static IndexRange Create(int min, int max)
		{
			return new IndexRange
			{
				min = min,
				max = max
			};
		}

		public int GetRandomValue(GameMath.RandType randomType)
		{
			return GameMath.GetRandomInt(this.min, this.max + 1, randomType);
		}

		public int min;

		public int max;
	}
}
