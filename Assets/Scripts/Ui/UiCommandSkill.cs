using System;
using Simulation;

namespace Ui
{
	public class UiCommandSkill : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.TryUseSkill(this.index);
		}

		public int index;
	}
}
