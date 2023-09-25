using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class ListGroupApplicationsResponse : PlayFabResultCommon
	{
		public List<GroupApplication> Applications;
	}
}
