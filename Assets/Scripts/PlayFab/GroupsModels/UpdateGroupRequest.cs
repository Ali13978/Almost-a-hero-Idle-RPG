using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class UpdateGroupRequest : PlayFabRequestCommon
	{
		public string AdminRoleId;

		public int? ExpectedProfileVersion;

		public EntityKey Group;

		public string GroupName;

		public string MemberRoleId;
	}
}
