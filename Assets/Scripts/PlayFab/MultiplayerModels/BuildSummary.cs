using System;
using System.Collections.Generic;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class BuildSummary
	{
		public string BuildId;

		public string BuildName;

		public DateTime? CreationTime;

		public Dictionary<string, string> Metadata;
	}
}
