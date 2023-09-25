using System;
using Simulation;

namespace Ui
{
	public class UiCommandLookedAtSpecialOffers : UiCommand
	{
		public override void Apply(Simulator sim)
		{
			sim.specialOfferBoard.hasAllSeen = true;
			if (sim.flashOfferBundle != null)
			{
				sim.flashOfferBundle.hasSeen = true;
			}
		}
	}
}
