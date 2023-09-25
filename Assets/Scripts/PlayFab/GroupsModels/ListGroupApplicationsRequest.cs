using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class ListGroupApplicationsRequest : PlayFabRequestCommon
	{
		public EntityKey Group;
	}
}
