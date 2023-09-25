using System;

namespace SA.IOSDeploy
{
	[Serializable]
	public class Flag
	{
		public bool IsOpen = true;

		public string Name;

		public FlagType Type;
	}
}
