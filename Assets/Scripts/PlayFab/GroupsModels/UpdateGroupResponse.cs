using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class UpdateGroupResponse : PlayFabResultCommon
	{
		public string OperationReason;

		public int ProfileVersion;

		public OperationTypes? SetResult;
	}
}
