using System;
using System.Collections.Generic;

namespace Simulation
{
	public class CalendarTreeOfferNode
	{
		public FlashOffer offer;

		public double offerAmount;

		public double offerCost;

		public KeyValuePair<int, int>[] dependencies;
	}
}
