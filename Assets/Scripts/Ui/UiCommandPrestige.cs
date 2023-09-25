using System;
using Simulation;

namespace Ui
{
	public class UiCommandPrestige : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.TryPrestige(this.isMega);
		}

		public bool isMega;
	}
}
