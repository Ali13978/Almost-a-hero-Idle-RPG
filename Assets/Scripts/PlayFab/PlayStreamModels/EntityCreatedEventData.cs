using System;

namespace PlayFab.PlayStreamModels
{
	public class EntityCreatedEventData : PlayStreamEventBase
	{
		public string EntityChain;

		public EntityLineage EntityLineage;
	}
}
