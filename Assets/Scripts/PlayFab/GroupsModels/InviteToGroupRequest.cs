using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class InviteToGroupRequest : PlayFabRequestCommon
	{
		public bool? AutoAcceptOutstandingApplication;

		public EntityKey Entity;

		public EntityKey Group;

		public string RoleId;
	}
}
