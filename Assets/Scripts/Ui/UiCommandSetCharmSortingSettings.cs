using System;
using Simulation;

namespace Ui
{
	public class UiCommandSetCharmSortingSettings : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.charmSortType = this.sortType;
			sim.isCharmSortingDescending = this.isDescending;
		}

		public CharmSortType sortType;

		public bool isDescending;
	}
}
