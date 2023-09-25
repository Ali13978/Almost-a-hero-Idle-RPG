using System;
using Simulation;

namespace Ui
{
	public class UICommandBuyFlashOffer : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.TryBuyFlashOffer(this.flashOffer, this.dropPosition);
		}

		public FlashOffer flashOffer;

		public DropPosition dropPosition;
	}
}
