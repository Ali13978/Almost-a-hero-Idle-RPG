using System;
using Simulation;

namespace Ui
{
	public class UiCommandUpgradeTotem : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.TryUpgradeTotem();
		}
	}
}
