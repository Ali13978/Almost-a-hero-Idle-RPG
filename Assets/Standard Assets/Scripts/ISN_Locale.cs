using System;

public class ISN_Locale
{
	public ISN_Locale(string countryCode, string contryName, string languageCode, string languageName)
	{
		this._CountryCode = countryCode;
		this._DisplayCountry = contryName;
		this._LanguageCode = languageCode;
		this._DisplayLanguage = languageName;
	}

	public string CountryCode
	{
		get
		{
			return this._CountryCode;
		}
	}

	public string DisplayCountry
	{
		get
		{
			return this._DisplayCountry;
		}
	}

	public string LanguageCode
	{
		get
		{
			return this._LanguageCode;
		}
	}

	public string DisplayLanguage
	{
		get
		{
			return this._DisplayLanguage;
		}
	}

	private string _CountryCode;

	private string _DisplayCountry;

	private string _LanguageCode;

	private string _DisplayLanguage;
}
