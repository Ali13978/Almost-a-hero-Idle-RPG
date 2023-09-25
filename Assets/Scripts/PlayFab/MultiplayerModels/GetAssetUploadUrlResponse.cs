using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class GetAssetUploadUrlResponse : PlayFabResultCommon
	{
		public string AssetUploadUrl;

		public string FileName;
	}
}
