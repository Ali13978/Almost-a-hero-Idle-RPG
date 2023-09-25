using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class DeleteCertificateRequest : PlayFabRequestCommon
	{
		public string Name;
	}
}
