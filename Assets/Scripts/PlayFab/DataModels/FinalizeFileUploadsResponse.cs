using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.DataModels
{
	[Serializable]
	public class FinalizeFileUploadsResponse : PlayFabResultCommon
	{
		public EntityKey Entity;

		public Dictionary<string, GetFileMetadata> Metadata;

		public int ProfileVersion;
	}
}
