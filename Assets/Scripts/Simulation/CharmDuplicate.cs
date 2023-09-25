using System;

namespace Simulation
{
	public class CharmDuplicate
	{
		public int count
		{
			get
			{
				return this.counts[0] + this.counts[1] + this.counts[2];
			}
		}

		public const int DupePhaseCount = 3;

		public CharmEffectData charmData;

		public int[] counts;
	}
}
