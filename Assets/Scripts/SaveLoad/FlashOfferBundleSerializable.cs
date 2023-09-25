using System;
using System.Collections.Generic;
using Simulation;

namespace SaveLoad
{
	[Serializable]
	public class FlashOfferBundleSerializable
	{
		public long timeCreated;

		public List<FlashOffer> offers;

		public List<FlashOffer> adventureOffers;

		public bool hasSeen;
	}
}
