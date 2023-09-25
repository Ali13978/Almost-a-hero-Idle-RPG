using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.DataModels
{
	[Serializable]
	public class InitiateFileUploadsResponse : PlayFabResultCommon
	{
		public EntityKey Entity;

		public int ProfileVersion;

		public List<InitiateFileUploadMetadata> UploadDetails;
	}
}
