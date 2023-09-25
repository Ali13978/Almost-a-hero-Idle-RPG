using System;
using Simulation;

namespace Ui
{
	public class UiCommandSelectCharmOffer : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			if (sim.IsActiveMode(GameMode.RIFT))
			{
				sim.TryBuyWorldUpgrade();
				sim.isActiveWorldPaused = false;
				sim.TryToClaimCharm(this.index);
				return;
			}
			throw new Exception("Invalid action: " + base.GetType());
		}

		public int index;
	}
}
