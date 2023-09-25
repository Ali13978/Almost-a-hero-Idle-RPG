using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class RemoveMembersRequest : PlayFabRequestCommon
	{
		public EntityKey Group;

		public List<EntityKey> Members;

		public string RoleId;
	}
}
