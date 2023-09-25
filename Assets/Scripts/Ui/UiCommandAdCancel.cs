using System;
using Simulation;
using Static;

namespace Ui
{
	public class UiCommandAdCancel : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.GetActiveWorld().OnAdCancel();
			PlayerStats.OnAdCancel();
		}
	}
}
