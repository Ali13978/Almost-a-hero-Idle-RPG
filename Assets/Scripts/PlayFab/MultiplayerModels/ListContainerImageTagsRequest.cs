using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class ListContainerImageTagsRequest : PlayFabRequestCommon
	{
		public string ImageName;
	}
}
