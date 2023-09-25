using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class ListMembershipOpportunitiesRequest : PlayFabRequestCommon
	{
		public EntityKey Entity;
	}
}
