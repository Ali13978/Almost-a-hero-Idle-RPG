using System;
using Simulation;

namespace Ui
{
	public class UICommandCharmPackOpen : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			if (this.isBigPack)
			{
				sim.OpenBigCharmPack();
			}
			else
			{
				sim.OpenSmallCharmPack();
			}
		}

		public bool isBigPack;
	}
}
