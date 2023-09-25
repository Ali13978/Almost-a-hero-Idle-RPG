using System;

namespace PlayFab.ClientModels
{
	[Serializable]
	public class UserSettings
	{
		public bool GatherDeviceInfo;

		public bool GatherFocusInfo;

		public bool NeedsAttribution;
	}
}
