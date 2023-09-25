using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.DataModels
{
	[Serializable]
	public class SetObjectsRequest : PlayFabRequestCommon
	{
		public EntityKey Entity;

		public int? ExpectedProfileVersion;

		public List<SetObject> Objects;
	}
}
