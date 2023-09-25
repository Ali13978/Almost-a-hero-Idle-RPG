using System;
using Simulation;

namespace Ui
{
	public class UiCommandCollectMine : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			if (TrustedTime.IsReady())
			{
				sim.TryCollectMine(this.mine, this.dropPos);
			}
		}

		public Mine mine;

		public DropPosition dropPos;
	}
}
