using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class DeleteBuildRequest : PlayFabRequestCommon
	{
		public string BuildId;
	}
}
