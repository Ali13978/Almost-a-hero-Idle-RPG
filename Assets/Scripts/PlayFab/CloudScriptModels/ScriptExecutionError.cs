using System;

namespace PlayFab.CloudScriptModels
{
	[Serializable]
	public class ScriptExecutionError
	{
		public string Error;

		public string Message;

		public string StackTrace;
	}
}
