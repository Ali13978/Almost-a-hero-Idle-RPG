using System;
using Simulation;

namespace Ui
{
	public class UiCommandTrinketTogglePin : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.TryPinTrinket(this.trinket);
		}

		public Trinket trinket;
	}
}
