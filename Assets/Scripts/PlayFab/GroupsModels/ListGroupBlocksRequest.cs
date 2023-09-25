using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class ListGroupBlocksRequest : PlayFabRequestCommon
	{
		public EntityKey Group;
	}
}
