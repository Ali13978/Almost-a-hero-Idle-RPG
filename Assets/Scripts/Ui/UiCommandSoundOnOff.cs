using System;
using Simulation;

namespace Ui
{
	public class UiCommandSoundOnOff : UiCommand
	{
		public override void Apply(Simulator sim)
		{
		}

		public override void Apply(Main main)
		{
			main.ToggleSound();
			main.ToggleVoices();
		}
	}
}
