using System;

namespace PlayFab.PlayStreamModels
{
	public class EntityExecutedCloudScriptEventData : PlayStreamEventBase
	{
		public ExecuteCloudScriptResult CloudScriptExecutionResult;

		public string EntityChain;

		public EntityLineage EntityLineage;

		public string FunctionName;
	}
}
