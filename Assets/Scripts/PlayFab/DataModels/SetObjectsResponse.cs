using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.DataModels
{
	[Serializable]
	public class SetObjectsResponse : PlayFabResultCommon
	{
		public int ProfileVersion;

		public List<SetObjectInfo> SetResults;
	}
}
