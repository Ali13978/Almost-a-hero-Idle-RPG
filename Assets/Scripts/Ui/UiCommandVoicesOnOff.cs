using System;
using Simulation;

namespace Ui
{
	public class UiCommandVoicesOnOff : UiCommand
	{
		public override void Apply(Simulator sim)
		{
		}

		public override void Apply(Main main)
		{
			main.ToggleVoices();
		}
	}
}
