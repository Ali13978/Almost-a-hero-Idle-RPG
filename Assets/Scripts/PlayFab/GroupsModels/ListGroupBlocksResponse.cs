using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class ListGroupBlocksResponse : PlayFabResultCommon
	{
		public List<GroupBlock> BlockedEntities;
	}
}
