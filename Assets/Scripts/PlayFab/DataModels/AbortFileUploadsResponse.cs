using System;
using PlayFab.SharedModels;

namespace PlayFab.DataModels
{
	[Serializable]
	public class AbortFileUploadsResponse : PlayFabResultCommon
	{
		public EntityKey Entity;

		public int ProfileVersion;
	}
}
