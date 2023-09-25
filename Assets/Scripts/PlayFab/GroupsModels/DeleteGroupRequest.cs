using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class DeleteGroupRequest : PlayFabRequestCommon
	{
		public EntityKey Group;
	}
}
