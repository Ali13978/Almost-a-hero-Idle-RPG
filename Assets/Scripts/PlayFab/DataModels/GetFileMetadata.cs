using System;

namespace PlayFab.DataModels
{
	[Serializable]
	public class GetFileMetadata
	{
		public string Checksum;

		public string DownloadUrl;

		public string FileName;

		public DateTime LastModified;

		public int Size;
	}
}
