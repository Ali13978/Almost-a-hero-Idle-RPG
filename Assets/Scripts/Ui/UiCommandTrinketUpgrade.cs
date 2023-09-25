using System;
using Simulation;

namespace Ui
{
	public class UiCommandTrinketUpgrade : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.TryUpgradeTrinket(this.trinket, this.effectIndex);
		}

		public Trinket trinket;

		public int effectIndex;
	}
}
