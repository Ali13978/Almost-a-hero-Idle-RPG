using System;

namespace PlayFab.DataModels
{
	[Serializable]
	public class SetObjectInfo
	{
		public string ObjectName;

		public string OperationReason;

		public OperationTypes? SetResult;
	}
}
