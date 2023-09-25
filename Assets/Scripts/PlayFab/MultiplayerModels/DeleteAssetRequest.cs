using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class DeleteAssetRequest : PlayFabRequestCommon
	{
		public string FileName;
	}
}
