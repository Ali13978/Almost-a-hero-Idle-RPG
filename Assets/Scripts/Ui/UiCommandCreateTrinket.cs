using System;
using Simulation;

namespace Ui
{
	public class UiCommandCreateTrinket : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.CreateTrinket(this.req, this.common, this.secondary, this.special);
		}

		public TrinketUpgradeReq req;

		public TrinketEffect common;

		public TrinketEffect secondary;

		public TrinketEffect special;
	}
}
