using System;
using System.Collections.Generic;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class AssetSummary
	{
		public string FileName;

		public Dictionary<string, string> Metadata;
	}
}
