using System;

namespace PlayFab.PlayStreamModels
{
	public class PlayerDataExportedEventData : PlayStreamEventBase
	{
		public string ExportDownloadUrl;

		public string JobReceiptId;

		public DateTime RequestTime;

		public string TitleId;
	}
}
