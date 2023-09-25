using System;
using Simulation;

namespace Ui
{
	public class UiCommandCraftTrinket : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.TryCraftTrinket(this.common, this.secondary, this.special, this.bodyIndex, this.colorIndex);
		}

		public TrinketEffect common;

		public TrinketEffect secondary;

		public TrinketEffect special;

		public int bodyIndex;

		public int colorIndex;
	}
}
