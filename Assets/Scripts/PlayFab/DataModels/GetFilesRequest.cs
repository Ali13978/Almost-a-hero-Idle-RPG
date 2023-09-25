using System;
using PlayFab.SharedModels;

namespace PlayFab.DataModels
{
	[Serializable]
	public class GetFilesRequest : PlayFabRequestCommon
	{
		public EntityKey Entity;
	}
}
