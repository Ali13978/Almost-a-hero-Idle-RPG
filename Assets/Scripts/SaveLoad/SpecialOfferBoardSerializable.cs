using System;
using System.Collections.Generic;

namespace SaveLoad
{
	[Serializable]
	public class SpecialOfferBoardSerializable
	{
		public int previousStandardOffer;

		public SpecialOfferKeeperSeralizable standardOffer;

		public int previousReAppearOffer;

		public SpecialOfferKeeperSeralizable reAppearingUniqueOffer;

		public List<SpecialOfferKeeperSeralizable> uniqueOffers;

		public bool hasAllSeen;

		public bool hasAllSeenOutOfShop;
	}
}
