using System;
using PlayFab.SharedModels;

namespace PlayFab.DataModels
{
	[Serializable]
	public class GetObjectsRequest : PlayFabRequestCommon
	{
		public EntityKey Entity;

		public bool? EscapeObject;
	}
}
