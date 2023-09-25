using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class UpdateGroupRoleResponse : PlayFabResultCommon
	{
		public string OperationReason;

		public int ProfileVersion;

		public OperationTypes? SetResult;
	}
}
