using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class ListContainerImagesResponse : PlayFabResultCommon
	{
		public List<string> Images;

		public int PageSize;

		public string SkipToken;
	}
}
