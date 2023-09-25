using System;
using PlayFab.SharedModels;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class IsMemberResponse : PlayFabResultCommon
	{
		public bool IsMember;
	}
}
