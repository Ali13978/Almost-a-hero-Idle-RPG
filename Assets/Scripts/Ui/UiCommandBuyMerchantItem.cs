using System;
using Simulation;

namespace Ui
{
	public class UiCommandBuyMerchantItem : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.TryBuyMerchantItem(this.index, this.count, this.isEventMerchantItem);
		}

		public int index;

		public bool isEventMerchantItem;

		public int count;
	}
}
