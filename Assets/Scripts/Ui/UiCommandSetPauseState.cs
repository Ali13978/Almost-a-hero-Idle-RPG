using System;
using Simulation;

namespace Ui
{
	public class UiCommandSetPauseState : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.isActiveWorldPaused = this.isPaused;
		}

		public bool isPaused;
	}
}
