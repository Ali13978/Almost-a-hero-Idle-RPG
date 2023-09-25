using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class CreateGroupRequest : PlayFabRequestCommon
	{
		public EntityKey Entity;

		public string GroupName;
	}
}
