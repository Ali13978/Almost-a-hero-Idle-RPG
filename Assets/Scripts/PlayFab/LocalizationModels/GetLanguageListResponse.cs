using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.LocalizationModels
{
	[Serializable]
	public class GetLanguageListResponse : PlayFabResultCommon
	{
		public List<string> LanguageList;
	}
}
