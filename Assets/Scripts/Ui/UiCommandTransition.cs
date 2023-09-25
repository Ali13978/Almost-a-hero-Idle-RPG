using System;
using Simulation;

namespace Ui
{
	public class UiCommandTransition : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.GetActiveWorld().OnEnvironmentTransitionUiWindowClosed();
		}
	}
}
