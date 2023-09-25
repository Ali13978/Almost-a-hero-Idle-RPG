using System;
using System.Collections.Generic;

namespace PlayFab.PlayStreamModels
{
	public class EntityObjectsSetEventData : PlayStreamEventBase
	{
		public string EntityChain;

		public EntityLineage EntityLineage;

		public List<ObjectSet> Objects;
	}
}
