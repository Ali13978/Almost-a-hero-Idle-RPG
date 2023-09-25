using System;
using System.Collections.Generic;

namespace Simulation
{
	public class FlashOfferBundle
	{
		public bool HasExpired()
		{
			return this.GetRemainingDur() <= 0.0;
		}

		public double GetRemainingDur()
		{
			double num = 39600.0;
			return num - (TrustedTime.Get() - this.timeCreated).TotalSeconds;
		}

		public FlashOffer GetSkinOfferWithId(int skinId)
		{
			return this.adventureOffers.Find((FlashOffer o) => (o.type == FlashOffer.Type.COSTUME || o.type == FlashOffer.Type.COSTUME_PLUS_SCRAP) && o.genericIntId == skinId);
		}

		public const double OFFER_RENEW_PERIOD = 39600.0;

		public const int NUM_OFFERS = 3;

		public DateTime timeCreated;

		public List<FlashOffer> offers;

		public List<FlashOffer> adventureOffers;

		public bool hasSeen;
	}
}
