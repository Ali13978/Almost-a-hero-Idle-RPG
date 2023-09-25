using System;
using Simulation;

namespace Ui
{
	public class UiCommandBuyWorldUpgrade : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.TryBuyWorldUpgrade();
		}
	}
}
