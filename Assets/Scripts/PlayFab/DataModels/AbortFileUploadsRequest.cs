using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.DataModels
{
	[Serializable]
	public class AbortFileUploadsRequest : PlayFabRequestCommon
	{
		public EntityKey Entity;

		public List<string> FileNames;

		public int? ProfileVersion;
	}
}
