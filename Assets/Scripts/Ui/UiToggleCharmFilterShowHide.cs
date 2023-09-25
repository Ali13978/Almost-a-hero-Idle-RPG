using System;
using Simulation;

namespace Ui
{
	public class UiToggleCharmFilterShowHide : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.isCharmSortingShowing = !sim.isCharmSortingShowing;
		}
	}
}
