using System;
using System.Collections.Generic;
using System.Globalization;
using Ui;
using UnityEngine;

public class LM
{
	public static void Initialize(ParsedLoc parsedLoc)
	{
		if (LM.isInitialized)
		{
			return;
		}
		LM.isInitialized = true;
		LM.data = new Dictionary<string, Dictionary<string, string>>();
		LM.availableLanguages = new List<Language>();
		LM.availableLanguages.Add(Language.EN);
		LM.availableLanguages.Add(Language.TR);
		LM.availableLanguages.Add(Language.ES);
		LM.availableLanguages.Add(Language.IT);
		LM.availableLanguages.Add(Language.FR);
		LM.availableLanguages.Add(Language.DE);
		LM.availableLanguages.Add(Language.PT);
		LM.availableLanguages.Add(Language.RU);
		LM.availableLanguages.Add(Language.ZH);
		LM.availableLanguages.Add(Language.JP);
		LM.availableLanguages.Add(Language.KR);
		LM.resizableLanguages = new List<Language>();
		LM.resizableLanguages.Add(Language.EN);
		LM.resizableLanguages.Add(Language.TR);
		LM.resizableLanguages.Add(Language.ES);
		LM.resizableLanguages.Add(Language.IT);
		LM.resizableLanguages.Add(Language.FR);
		LM.resizableLanguages.Add(Language.DE);
		LM.resizableLanguages.Add(Language.PT);
		LM.resizableLanguages.Add(Language.RU);
		int i = 0;
		int count = parsedLoc.languages.Count;
		while (i < count)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			int j = 0;
			int count2 = parsedLoc.keys.Count;
			while (j < count2)
			{
				if (dictionary.ContainsKey(parsedLoc.keys[j]))
				{
					UnityEngine.Debug.Log(parsedLoc.keys[j] + " is already contained.");
				}
				else
				{
					dictionary.Add(parsedLoc.keys[j], parsedLoc.locValues[i].values[j]);
				}
				j++;
			}
			LM.data.Add(parsedLoc.languages[i], dictionary);
			i++;
		}
		LM.SelectLanguage(LM.GetSystemLanguageId());
	}

	public static void SelectLanguage(Language language)
	{
		if (!LM.availableLanguages.Contains(language))
		{
			language = Language.EN;
		}
		string languageID = LM.GetLanguageID(language);
		LM.current = LM.data[languageID];
		LM.currentLanguage = language;
		string name = languageID.Replace('_', '-');
		LM.culture = CultureInfo.GetCultureInfo(name);
		LM.uiShouldInitStrings = true;
	}

	public static string Get(string id)
	{
		string text;
		if (!LM.current.TryGetValue(id, out text))
		{
			return "#" + id;
		}
		if (!string.IsNullOrEmpty(text))
		{
			return text.Replace("</br>", "\n");
		}
		LM.data[LanguageID.EN].TryGetValue(id, out text);
		if (string.IsNullOrEmpty(text))
		{
			return "<color=red>#UNSET_</color>" + id;
		}
		return "<color=red>#UNSET_</color>" + text;
	}

	public static string Get(string id, object format)
	{
		return string.Format(LM.Get(id), format);
	}

	public static string GetFromEN(string id)
	{
		if (LM.data[LanguageID.EN].ContainsKey(id))
		{
			return LM.data[LanguageID.EN][id];
		}
		return "#" + id;
	}

	public static Language GetNextLanguage()
	{
		int i = 0;
		int count = LM.availableLanguages.Count;
		while (i < count)
		{
			if (LM.availableLanguages[i] == LM.currentLanguage)
			{
				if (i < count - 1)
				{
					return LM.availableLanguages[i + 1];
				}
				return LM.availableLanguages[0];
			}
			else
			{
				i++;
			}
		}
		return Language.EN;
	}

	public static Language GetSystemLanguageId()
	{
		Language result = Language.EN;
		SystemLanguage systemLanguage = Application.systemLanguage;
		switch (systemLanguage)
		{
		case SystemLanguage.Italian:
			result = Language.IT;
			break;
		case SystemLanguage.Japanese:
			result = Language.JP;
			break;
		case SystemLanguage.Korean:
			result = Language.KR;
			break;
		default:
			if (systemLanguage != SystemLanguage.French)
			{
				if (systemLanguage != SystemLanguage.German)
				{
					switch (systemLanguage)
					{
					case SystemLanguage.Spanish:
						result = Language.ES;
						break;
					default:
						if (systemLanguage != SystemLanguage.ChineseSimplified && systemLanguage != SystemLanguage.ChineseTraditional && systemLanguage != SystemLanguage.Chinese)
						{
							if (systemLanguage == SystemLanguage.English)
							{
								result = Language.EN;
							}
						}
						else
						{
							result = Language.ZH;
						}
						break;
					case SystemLanguage.Turkish:
						result = Language.TR;
						break;
					}
				}
				else
				{
					result = Language.DE;
				}
			}
			else
			{
				result = Language.FR;
			}
			break;
		case SystemLanguage.Portuguese:
			result = Language.PT;
			break;
		case SystemLanguage.Russian:
			result = Language.RU;
			break;
		}
		return result;
	}

	public static string GetLanguageID(Language language)
	{
		switch (language)
		{
		case Language.EN:
			return LanguageID.EN;
		case Language.TR:
			return LanguageID.TR;
		case Language.ES:
			return LanguageID.ES;
		case Language.IT:
			return LanguageID.IT;
		case Language.FR:
			return LanguageID.FR;
		case Language.DE:
			return LanguageID.DE;
		case Language.PT:
			return LanguageID.PT;
		case Language.RU:
			return LanguageID.RU;
		case Language.ZH:
			return LanguageID.ZH;
		case Language.JP:
			return LanguageID.JP;
		case Language.KR:
			return LanguageID.KR;
		default:
			throw new NotImplementedException();
		}
	}

	public static string GetLanguageID_N(Language language)
	{
		switch (language)
		{
		case Language.EN:
			return LanguageID.EN_N;
		case Language.TR:
			return LanguageID.TR_N;
		case Language.ES:
			return LanguageID.ES_N;
		case Language.IT:
			return LanguageID.IT_N;
		case Language.FR:
			return LanguageID.FR_N;
		case Language.DE:
			return LanguageID.DE_N;
		case Language.PT:
			return LanguageID.PT_N;
		case Language.RU:
			return LanguageID.RU_N;
		case Language.ZH:
			return LanguageID.ZH_N;
		case Language.JP:
			return LanguageID.JP_N;
		case Language.KR:
			return LanguageID.KR_N;
		default:
			throw new NotImplementedException();
		}
	}

	public static bool CanResize()
	{
		return LM.resizableLanguages.Contains(LM.currentLanguage);
	}

	public static Dictionary<string, Dictionary<string, string>> data;

	public static Dictionary<string, string> current;

	public static Language currentLanguage;

	public static CultureInfo culture;

	public static List<Language> availableLanguages;

	public static List<Language> resizableLanguages;

	public static bool uiShouldInitStrings;

	private static bool isInitialized;
}
