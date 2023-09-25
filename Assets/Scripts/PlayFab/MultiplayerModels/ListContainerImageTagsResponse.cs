using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class ListContainerImageTagsResponse : PlayFabResultCommon
	{
		public List<string> Tags;
	}
}
