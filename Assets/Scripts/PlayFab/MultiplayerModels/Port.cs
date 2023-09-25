using System;

namespace PlayFab.MultiplayerModels
{
	[Serializable]
	public class Port
	{
		public string Name;

		public int Num;

		public ProtocolType Protocol;
	}
}
