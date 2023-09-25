using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class GetAssetUploadUrlRequest : PlayFabRequestCommon
	{
		public string FileName;
	}
}
