using System;
using Simulation;

namespace Ui
{
	public class UiCommandOpenCharmPack : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.OpenRandomCardPack(this.count);
		}

		public int count;
	}
}
