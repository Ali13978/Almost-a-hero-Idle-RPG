using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class UnblockEntityRequest : PlayFabRequestCommon
	{
		public EntityKey Entity;

		public EntityKey Group;
	}
}
