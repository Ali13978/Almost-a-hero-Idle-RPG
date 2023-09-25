using System;
using Simulation;

namespace Ui
{
	public class UiCommandUnlockCollectReward : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.TryCollectUnlockReward(this.unlock);
		}

		public Unlock unlock;
	}
}
