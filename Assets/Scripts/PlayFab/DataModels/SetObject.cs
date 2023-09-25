using System;

namespace PlayFab.DataModels
{
	[Serializable]
	public class SetObject
	{
		public object DataObject;

		public bool? DeleteObject;

		public string EscapedDataObject;

		public string ObjectName;
	}
}
