using System;
using PlayFab.SharedModels;

namespace PlayFab.DataModels
{
	[Serializable]
	public class ObjectResult : PlayFabResultCommon
	{
		public object DataObject;

		public string EscapedDataObject;

		public string ObjectName;
	}
}
