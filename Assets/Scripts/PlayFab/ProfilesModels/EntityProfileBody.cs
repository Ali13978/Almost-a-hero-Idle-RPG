using System;
using System.Collections.Generic;

namespace PlayFab.ProfilesModels
{
	[Serializable]
	public class EntityProfileBody
	{
		public EntityKey Entity;

		public string EntityChain;

		public Dictionary<string, EntityProfileFileMetadata> Files;

		public string FriendlyName;

		public string Language;

		public EntityLineage Lineage;

		public Dictionary<string, EntityDataObject> Objects;

		public List<EntityPermissionStatement> Permissions;

		public int VersionNumber;
	}
}
