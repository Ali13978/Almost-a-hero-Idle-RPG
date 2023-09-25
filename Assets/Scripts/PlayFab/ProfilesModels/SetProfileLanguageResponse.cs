using System;
using PlayFab.SharedModels;

namespace PlayFab.ProfilesModels
{
	[Serializable]
	public class SetProfileLanguageResponse : PlayFabResultCommon
	{
		public OperationTypes? OperationResult;

		public int? VersionNumber;
	}
}
