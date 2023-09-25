using System;
using Simulation;

namespace Ui
{
	public class UiCommandBoss : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			if (sim.CanGoToBoss())
			{
				sim.TryGoToBoss();
			}
			else
			{
				sim.TryLeaveBoss();
			}
		}
	}
}
