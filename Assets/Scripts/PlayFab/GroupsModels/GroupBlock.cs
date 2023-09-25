using System;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class GroupBlock
	{
		public EntityWithLineage Entity;

		public EntityKey Group;
	}
}
