using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class ListMembershipRequest : PlayFabRequestCommon
	{
		public EntityKey Entity;
	}
}
