using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class RemoveGroupApplicationRequest : PlayFabRequestCommon
	{
		public EntityKey Entity;

		public EntityKey Group;
	}
}
