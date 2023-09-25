using System;
using Simulation;

namespace Ui
{
	public class UiCommandCollectQuestOfUpdate : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.TryToCollectQuestOfUpdate(this.dropPosition);
		}

		public DropPosition dropPosition;
	}
}
