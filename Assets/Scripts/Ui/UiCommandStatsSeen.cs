using System;
using Simulation;

namespace Ui
{
	public class UiCommandStatsSeen : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.newStats.Clear();
		}
	}
}
