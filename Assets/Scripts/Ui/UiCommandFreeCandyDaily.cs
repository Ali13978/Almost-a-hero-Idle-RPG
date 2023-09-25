using System;
using Simulation;

namespace Ui
{
	public class UiCommandFreeCandyDaily : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			World activeWorld = sim.GetActiveWorld();
			activeWorld.RainCurrencyOnUi(UiState.HUB_SHOP, CurrencyType.CANDY, PlayfabManager.titleData.christmasFreeCandiesAmount, this.dropPosition, 30, 0f, 0f, 1f, null, 0f);
			sim.lastFreeCandyTreatClaimedDate = TrustedTime.Get();
			sim.christmasFreeCandyNotificationSeen = false;
		}

		public DropPosition dropPosition;
	}
}
