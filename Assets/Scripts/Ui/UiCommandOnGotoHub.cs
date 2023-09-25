using System;
using Simulation;

namespace Ui
{
	public class UiCommandOnGotoHub : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			if (sim.GetActiveWorld().gameMode == GameMode.CRUSADE)
			{
				sim.TrySwitchGameMode(GameMode.STANDARD);
			}
		}
	}
}
