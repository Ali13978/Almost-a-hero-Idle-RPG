using System;
using Simulation;

namespace Ui
{
	public class UiCommandUpgradeGear : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.TryUpgradeGear(this.gear);
		}

		public Gear gear;
	}
}
