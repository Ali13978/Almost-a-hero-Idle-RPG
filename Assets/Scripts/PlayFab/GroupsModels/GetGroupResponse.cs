using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class GetGroupResponse : PlayFabResultCommon
	{
		public string AdminRoleId;

		public DateTime Created;

		public EntityKey Group;

		public string GroupName;

		public string MemberRoleId;

		public int ProfileVersion;

		public Dictionary<string, string> Roles;
	}
}
