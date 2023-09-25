using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class ListGroupMembersRequest : PlayFabRequestCommon
	{
		public EntityKey Group;
	}
}
