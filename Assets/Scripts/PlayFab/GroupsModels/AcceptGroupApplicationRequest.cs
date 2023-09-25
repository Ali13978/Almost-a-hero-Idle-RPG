using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class AcceptGroupApplicationRequest : PlayFabRequestCommon
	{
		public EntityKey Entity;

		public EntityKey Group;
	}
}
