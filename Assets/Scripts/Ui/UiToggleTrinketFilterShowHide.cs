using System;
using Simulation;

namespace Ui
{
	public class UiToggleTrinketFilterShowHide : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.isTrinketSortingShowing = !sim.isTrinketSortingShowing;
		}
	}
}
