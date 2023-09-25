using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class ApplyToGroupResponse : PlayFabResultCommon
	{
		public EntityWithLineage Entity;

		public DateTime Expires;

		public EntityKey Group;
	}
}
