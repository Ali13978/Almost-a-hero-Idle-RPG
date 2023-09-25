using System;

namespace PlayFab.PlayStreamModels
{
	public class GroupUpdatedEventData : PlayStreamEventBase
	{
		public string EntityChain;

		public EntityLineage EntityLineage;

		public string GroupName;

		public GroupPropertyValues NewValues;

		public GroupPropertyValues OldValues;

		public string UpdaterEntityId;

		public string UpdaterEntityType;
	}
}
