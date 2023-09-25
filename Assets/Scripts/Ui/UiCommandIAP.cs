using System;
using Simulation;

namespace Ui
{
	public class UiCommandIAP : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			IapManager.inst.Buy(this.index);
		}

		public int index;
	}
}
