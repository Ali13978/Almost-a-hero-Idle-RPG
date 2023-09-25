using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class UploadCertificateRequest : PlayFabRequestCommon
	{
		public Certificate GameCertificate;
	}
}
