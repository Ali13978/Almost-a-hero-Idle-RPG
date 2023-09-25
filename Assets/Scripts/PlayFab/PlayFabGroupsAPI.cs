using System;
using System.Collections.Generic;
using PlayFab.GroupsModels;
using PlayFab.Internal;

namespace PlayFab
{
	public static class PlayFabGroupsAPI
	{
		public static void ForgetAllCredentials()
		{
			PlayFabHttp.ForgetAllCredentials();
		}

		public static void AcceptGroupApplication(AcceptGroupApplicationRequest request, Action<EmptyResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<EmptyResponse>("/Group/AcceptGroupApplication", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void AcceptGroupInvitation(AcceptGroupInvitationRequest request, Action<EmptyResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<EmptyResponse>("/Group/AcceptGroupInvitation", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void AddMembers(AddMembersRequest request, Action<EmptyResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<EmptyResponse>("/Group/AddMembers", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ApplyToGroup(ApplyToGroupRequest request, Action<ApplyToGroupResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<ApplyToGroupResponse>("/Group/ApplyToGroup", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void BlockEntity(BlockEntityRequest request, Action<EmptyResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<EmptyResponse>("/Group/BlockEntity", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ChangeMemberRole(ChangeMemberRoleRequest request, Action<EmptyResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<EmptyResponse>("/Group/ChangeMemberRole", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void CreateGroup(CreateGroupRequest request, Action<CreateGroupResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<CreateGroupResponse>("/Group/CreateGroup", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void CreateRole(CreateGroupRoleRequest request, Action<CreateGroupRoleResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<CreateGroupRoleResponse>("/Group/CreateRole", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void DeleteGroup(DeleteGroupRequest request, Action<EmptyResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<EmptyResponse>("/Group/DeleteGroup", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void DeleteRole(DeleteRoleRequest request, Action<EmptyResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<EmptyResponse>("/Group/DeleteRole", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void GetGroup(GetGroupRequest request, Action<GetGroupResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<GetGroupResponse>("/Group/GetGroup", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void InviteToGroup(InviteToGroupRequest request, Action<InviteToGroupResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<InviteToGroupResponse>("/Group/InviteToGroup", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void IsMember(IsMemberRequest request, Action<IsMemberResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<IsMemberResponse>("/Group/IsMember", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ListGroupApplications(ListGroupApplicationsRequest request, Action<ListGroupApplicationsResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<ListGroupApplicationsResponse>("/Group/ListGroupApplications", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ListGroupBlocks(ListGroupBlocksRequest request, Action<ListGroupBlocksResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<ListGroupBlocksResponse>("/Group/ListGroupBlocks", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ListGroupInvitations(ListGroupInvitationsRequest request, Action<ListGroupInvitationsResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<ListGroupInvitationsResponse>("/Group/ListGroupInvitations", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ListGroupMembers(ListGroupMembersRequest request, Action<ListGroupMembersResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<ListGroupMembersResponse>("/Group/ListGroupMembers", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ListMembership(ListMembershipRequest request, Action<ListMembershipResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<ListMembershipResponse>("/Group/ListMembership", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void ListMembershipOpportunities(ListMembershipOpportunitiesRequest request, Action<ListMembershipOpportunitiesResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<ListMembershipOpportunitiesResponse>("/Group/ListMembershipOpportunities", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RemoveGroupApplication(RemoveGroupApplicationRequest request, Action<EmptyResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<EmptyResponse>("/Group/RemoveGroupApplication", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RemoveGroupInvitation(RemoveGroupInvitationRequest request, Action<EmptyResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<EmptyResponse>("/Group/RemoveGroupInvitation", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void RemoveMembers(RemoveMembersRequest request, Action<EmptyResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<EmptyResponse>("/Group/RemoveMembers", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UnblockEntity(UnblockEntityRequest request, Action<EmptyResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<EmptyResponse>("/Group/UnblockEntity", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateGroup(UpdateGroupRequest request, Action<UpdateGroupResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<UpdateGroupResponse>("/Group/UpdateGroup", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}

		public static void UpdateRole(UpdateGroupRoleRequest request, Action<UpdateGroupRoleResponse> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
		{
			PlayFabHttp.MakeApiCall<UpdateGroupRoleResponse>("/Group/UpdateRole", request, AuthType.EntityToken, resultCallback, errorCallback, customData, extraHeaders, false);
		}
	}
}
