using System;
using System.Collections.Generic;
using Simulation;

namespace SaveLoad
{
	[Serializable]
	public class ServerSideFlashOfferBundleSerializable
	{
		public List<FlashOffer> offers;

		public bool hasSeen;
	}
}
