using System;
using Simulation;

namespace Ui
{
	public class UiCommandCollectOfflineEarnings : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.TryCollectOfflineEarnings();
		}
	}
}
