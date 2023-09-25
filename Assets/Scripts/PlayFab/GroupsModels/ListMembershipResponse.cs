using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class ListMembershipResponse : PlayFabResultCommon
	{
		public List<GroupWithRoles> Groups;
	}
}
