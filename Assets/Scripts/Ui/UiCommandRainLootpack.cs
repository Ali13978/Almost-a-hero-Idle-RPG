using System;
using Simulation;

namespace Ui
{
	public class UiCommandRainLootpack : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.TryOpenLootpack(this.shopPack);
		}

		public ShopPack shopPack;
	}
}
