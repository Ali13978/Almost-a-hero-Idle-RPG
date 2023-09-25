using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class ListGroupInvitationsResponse : PlayFabResultCommon
	{
		public List<GroupInvitation> Invitations;
	}
}
