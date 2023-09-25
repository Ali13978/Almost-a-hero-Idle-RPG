using System;
using Simulation;

namespace Ui
{
	public class UiCommandLookedAtOutOfShopOffers : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.specialOfferBoard.hasAllSeenOutOfShop = true;
			if (sim.secondAnniversaryFlashOffersBundle != null)
			{
				sim.secondAnniversaryFlashOffersBundle.hasSeen = true;
			}
		}
	}
}
