using System;
using Simulation;

namespace Ui
{
	public class UiCommandGameModeSwitch : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.TrySwitchGameMode(this.toMode);
		}

		public GameMode toMode;
	}
}
