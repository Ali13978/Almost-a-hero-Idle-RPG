using System;
using PlayFab.ClientModels;

namespace Simulation
{
	public class ShopPackRegional01Brazil : ShopPackRegional01
	{
		public ShopPackRegional01Brazil()
		{
			this.timeStart = new DateTime(2018, 9, 5, 10, 0, 0);
			this.countryCodes = new CountryCode[]
			{
				CountryCode.BR
			};
		}
	}
}
