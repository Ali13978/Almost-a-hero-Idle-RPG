using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.DataModels
{
	[Serializable]
	public class GetFilesResponse : PlayFabResultCommon
	{
		public EntityKey Entity;

		public Dictionary<string, GetFileMetadata> Metadata;

		public int ProfileVersion;
	}
}
