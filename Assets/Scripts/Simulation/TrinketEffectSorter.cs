using System;
using System.Collections.Generic;

namespace Simulation
{
	public class TrinketEffectSorter : IComparer<TrinketEffect>
	{
		public int Compare(TrinketEffect x, TrinketEffect y)
		{
			int group = (int)x.GetGroup();
			int group2 = (int)y.GetGroup();
			return group.CompareTo(group2);
		}
	}
}
