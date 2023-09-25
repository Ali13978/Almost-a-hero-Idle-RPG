using System;
using System.Collections.Generic;

namespace PlayFab.PlayStreamModels
{
	public class EntityFilesSetEventData : PlayStreamEventBase
	{
		public string EntityChain;

		public EntityLineage EntityLineage;

		public List<FileSet> Files;
	}
}
