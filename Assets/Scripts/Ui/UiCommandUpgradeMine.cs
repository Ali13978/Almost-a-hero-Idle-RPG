using System;
using Simulation;

namespace Ui
{
	public class UiCommandUpgradeMine : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.TryUpgradeMine(this.mine);
		}

		public Mine mine;
	}
}
