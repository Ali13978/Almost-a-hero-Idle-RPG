using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class ListMembershipOpportunitiesResponse : PlayFabResultCommon
	{
		public List<GroupApplication> Applications;

		public List<GroupInvitation> Invitations;
	}
}
