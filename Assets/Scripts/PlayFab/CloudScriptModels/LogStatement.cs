using System;

namespace PlayFab.CloudScriptModels
{
	[Serializable]
	public class LogStatement
	{
		public object Data;

		public string Level;

		public string Message;
	}
}
