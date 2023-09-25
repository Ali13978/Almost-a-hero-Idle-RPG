using System;
using PlayFab.SharedModels;

namespace PlayFab.AuthenticationModels
{
	[Serializable]
	public class GetEntityTokenResponse : PlayFabResultCommon
	{
		public EntityKey Entity;

		public string EntityToken;

		public DateTime? TokenExpiration;
	}
}
