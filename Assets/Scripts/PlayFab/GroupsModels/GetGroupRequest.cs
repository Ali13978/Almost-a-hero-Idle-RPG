using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class GetGroupRequest : PlayFabRequestCommon
	{
		public EntityKey Group;

		public string GroupName;
	}
}
