using System;
using System.Collections.Generic;

namespace PlayFab.GroupsModels
{
	[Serializable]
	public class EntityWithLineage
	{
		public EntityKey Key;

		public Dictionary<string, EntityKey> Lineage;
	}
}
