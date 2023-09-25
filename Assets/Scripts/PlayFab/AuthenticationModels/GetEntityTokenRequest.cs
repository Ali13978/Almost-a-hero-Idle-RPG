using System;
using PlayFab.SharedModels;

namespace PlayFab.AuthenticationModels
{
	[Serializable]
	public class GetEntityTokenRequest : PlayFabRequestCommon
	{
		public EntityKey Entity;
	}
}
