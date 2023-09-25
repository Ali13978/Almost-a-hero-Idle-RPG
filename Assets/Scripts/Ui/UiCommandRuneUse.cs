using System;
using Simulation;

namespace Ui
{
	public class UiCommandRuneUse : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.TryUseRune(sim.GetRune(this.runeId));
		}

		public string runeId;
	}
}
