using System;
using Simulation;

namespace Ui
{
	public class UiCommandOpenTrinketPack : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.TryOpenTrinketPack(this.shopPacktrinket, this.isSpecial);
		}

		public ShopPackTrinket shopPacktrinket;

		public bool isSpecial;
	}
}
