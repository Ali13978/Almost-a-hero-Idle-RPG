using System;
using PlayFab.ClientModels;

namespace Simulation
{
	public class ShopPackRegional01Mexico : ShopPackRegional01
	{
		public ShopPackRegional01Mexico()
		{
			this.timeStart = new DateTime(2018, 9, 12, 10, 0, 0);
			this.countryCodes = new CountryCode[]
			{
				CountryCode.MX
			};
		}
	}
}
