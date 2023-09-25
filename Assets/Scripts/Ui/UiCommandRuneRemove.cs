using System;
using Simulation;

namespace Ui
{
	public class UiCommandRuneRemove : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.TryRemoveRune(sim.GetRune(this.runeId));
		}

		public string runeId;
	}
}
