using System;
using Simulation;

namespace Ui
{
	public class UiCommandNewHeroIconSelected : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.OnNewHeroIconSelected(this.heroId);
		}

		public string heroId;
	}
}
