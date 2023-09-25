using System;
using Simulation;

namespace Ui
{
	public class UICommandCharmSelected : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.allCharmEffects[this.id].isNew = false;
		}

		public int id;
	}
}
