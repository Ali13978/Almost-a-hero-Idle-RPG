using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class IsMemberRequest : PlayFabRequestCommon
	{
		public EntityKey Entity;

		public EntityKey Group;

		public string RoleId;
	}
}
