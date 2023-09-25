using System;

namespace PlayFab.ProfilesModels
{
	[Serializable]
	public class EntityProfileFileMetadata
	{
		public string Checksum;

		public string FileName;

		public DateTime LastModified;

		public int Size;
	}
}
