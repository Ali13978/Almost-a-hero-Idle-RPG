using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class InviteToGroupResponse : PlayFabResultCommon
	{
		public DateTime Expires;

		public EntityKey Group;

		public EntityWithLineage InvitedByEntity;

		public EntityWithLineage InvitedEntity;

		public string RoleId;
	}
}
