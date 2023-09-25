using System;

namespace PlayFab.PlayStreamModels
{
	public class CharacterVirtualCurrencyBalanceChangedEventData : PlayStreamEventBase
	{
		public string OrderId;

		public string PlayerId;

		public string TitleId;

		public int VirtualCurrencyBalance;

		public string VirtualCurrencyName;

		public int VirtualCurrencyPreviousBalance;
	}
}
