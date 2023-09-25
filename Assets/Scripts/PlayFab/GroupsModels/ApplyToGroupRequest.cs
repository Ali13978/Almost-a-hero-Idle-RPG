using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class ApplyToGroupRequest : PlayFabRequestCommon
	{
		public bool? AutoAcceptOutstandingInvite;

		public EntityKey Entity;

		public EntityKey Group;
	}
}
