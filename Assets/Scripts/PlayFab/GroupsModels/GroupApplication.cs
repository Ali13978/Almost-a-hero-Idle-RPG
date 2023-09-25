using System;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class GroupApplication
	{
		public EntityWithLineage Entity;

		public DateTime Expires;

		public EntityKey Group;
	}
}
