using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class GetBuildRequest : PlayFabRequestCommon
	{
		public string BuildId;
	}
}
