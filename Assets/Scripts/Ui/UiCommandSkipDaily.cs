using System;
using Simulation;

namespace Ui
{
	public class UiCommandSkipDaily : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			if (sim.CanSkipDailyQuest())
			{
				sim.SkipDailyQuest();
				return;
			}
			throw new Exception("Can not skip quest");
		}
	}
}
