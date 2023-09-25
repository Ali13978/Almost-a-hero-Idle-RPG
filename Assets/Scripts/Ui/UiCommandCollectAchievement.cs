using System;
using Simulation;

namespace Ui
{
	public class UiCommandCollectAchievement : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.TryCollectAchievement(this.id, this.dropPosition);
		}

		public string id;

		public DropPosition dropPosition;
	}
}
