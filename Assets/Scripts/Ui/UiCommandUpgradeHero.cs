using System;
using Simulation;

namespace Ui
{
	public class UiCommandUpgradeHero : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.TryUpgradeHeroWithIndex(this.index);
		}

		public int index;
	}
}
