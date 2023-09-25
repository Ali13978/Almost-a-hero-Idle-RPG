using System;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class GroupInvitation
	{
		public DateTime Expires;

		public EntityKey Group;

		public EntityWithLineage InvitedByEntity;

		public EntityWithLineage InvitedEntity;

		public string RoleId;
	}
}
