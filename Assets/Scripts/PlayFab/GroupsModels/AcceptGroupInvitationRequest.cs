using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class AcceptGroupInvitationRequest : PlayFabRequestCommon
	{
		public EntityKey Entity;

		public EntityKey Group;
	}
}
