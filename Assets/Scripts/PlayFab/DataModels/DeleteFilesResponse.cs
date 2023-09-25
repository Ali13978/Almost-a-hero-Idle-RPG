using System;
using PlayFab.SharedModels;

namespace PlayFab.DataModels
{
	[Serializable]
	public class DeleteFilesResponse : PlayFabResultCommon
	{
		public EntityKey Entity;

		public int ProfileVersion;
	}
}
