using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class BlockEntityRequest : PlayFabRequestCommon
	{
		public EntityKey Entity;

		public EntityKey Group;
	}
}
