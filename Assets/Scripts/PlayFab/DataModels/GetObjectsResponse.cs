using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.DataModels
{
	[Serializable]
	public class GetObjectsResponse : PlayFabResultCommon
	{
		public EntityKey Entity;

		public Dictionary<string, ObjectResult> Objects;

		public int ProfileVersion;
	}
}
