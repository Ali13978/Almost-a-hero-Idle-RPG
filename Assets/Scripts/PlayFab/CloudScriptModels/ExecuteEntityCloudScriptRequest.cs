using System;
using PlayFab.SharedModels;

namespace PlayFab.CloudScriptModels
{
	[Serializable]
	public class ExecuteEntityCloudScriptRequest : PlayFabRequestCommon
	{
		public EntityKey Entity;

		public string FunctionName;

		public object FunctionParameter;

		public bool? GeneratePlayStreamEvent;

		public CloudScriptRevisionOption? RevisionSelection;

		public int? SpecificRevision;
	}
}
