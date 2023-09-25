using System;

namespace PlayFab.PlayStreamModels
{
	public class GroupDeletedEventData : PlayStreamEventBase
	{
		public string DeleterEntityId;

		public string DeleterEntityType;

		public string EntityChain;

		public EntityLineage EntityLineage;

		public string GroupName;
	}
}
