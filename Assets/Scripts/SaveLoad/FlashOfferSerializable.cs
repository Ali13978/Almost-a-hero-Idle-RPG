using System;
using System.Collections.Generic;

namespace SaveLoad
{
	[Serializable]
	public class FlashOfferSerializable
	{
		public int type;

		public CurrencyType neededCurrency;

		public double price;

		public bool isBought;

		public List<object> otherVairables;
	}
}
