using System;
using Simulation;

namespace Ui
{
	public class UiCommandSetTrinketSortingSettings : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.trinketSortType = this.sortType;
			sim.isTrinketSortingDescending = this.isDescending;
		}

		public TrinketSortType sortType;

		public bool isDescending;
	}
}
