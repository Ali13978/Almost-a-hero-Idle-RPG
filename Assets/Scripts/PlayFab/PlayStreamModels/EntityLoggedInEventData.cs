using System;

namespace PlayFab.PlayStreamModels
{
	public class EntityLoggedInEventData : PlayStreamEventBase
	{
		public string EntityChain;

		public EntityLineage EntityLineage;
	}
}
