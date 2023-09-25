using System;
using Simulation;

namespace Ui
{
	public class UiCommandGetNextCharmDraft : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			if (sim.IsActiveMode(GameMode.RIFT))
			{
				sim.TryToPickNextCharmEffects();
				sim.isActiveWorldPaused = true;
				return;
			}
			throw new Exception("Invalid action: " + base.GetType());
		}
	}
}
