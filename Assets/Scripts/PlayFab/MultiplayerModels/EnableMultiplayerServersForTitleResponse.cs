using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class EnableMultiplayerServersForTitleResponse : PlayFabResultCommon
	{
		public TitleMultiplayerServerEnabledStatus? Status;
	}
}
