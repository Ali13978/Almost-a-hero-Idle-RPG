using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class ListGroupInvitationsRequest : PlayFabRequestCommon
	{
		public EntityKey Group;
	}
}
