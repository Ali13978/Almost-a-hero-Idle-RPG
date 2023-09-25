using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class ListVirtualMachineSummariesResponse : PlayFabResultCommon
	{
		public int PageSize;

		public string SkipToken;

		public List<VirtualMachineSummary> VirtualMachines;
	}
}
