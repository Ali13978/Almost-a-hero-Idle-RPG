using System;
using PlayFab.SharedModels;

namespace PlayFab.ProfilesModels
{
	[Serializable]
	public class SetProfileLanguageRequest : PlayFabRequestCommon
	{
		public EntityKey Entity;

		public int ExpectedVersion;

		public string Language;
	}
}
