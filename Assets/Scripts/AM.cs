using System;
using Static;
using UnityEngine;

public static class AM
{
	static AM()
	{
		AM.colorHexWhite = StringExtension.Concat("<color=#", "FFFFFFFF", ">");
		AM.colorHexGreen = StringExtension.Concat("<color=#", "E0FB18FF", ">");
		AM.colorHexAmount = StringExtension.Concat("<color=#", AM.ColorToHex(DC.amount), ">");
		AM.colorHexTime = StringExtension.Concat("<color=#", AM.ColorToHex(DC.time), ">");
		AM.colorHexLevel = StringExtension.Concat("<color=#", AM.ColorToHex(DC.level), ">");
		AM.colorHexMerchant = StringExtension.Concat("<color=#", AM.ColorToHex(DC.merchant), ">");
		AM.colorHexRuneBold = StringExtension.Concat("<color=#", AM.ColorToHex(DC.runeBold), ">");
		AM.colorHexRuneInc = StringExtension.Concat("<color=#", AM.ColorToHex(DC.runeInc), ">");
		AM.colorHexArtifactUpgrade = StringExtension.Concat("<color=#", AM.ColorToHex(DC.artifactUpgrade), ">");
		AM.colorHexGenericDarkBrown = StringExtension.Concat("<color=#", "554129FF", ">");
		AM.colorHexCharmDescriptionUpgrade = StringExtension.Concat("<color=#", "6C7E00FF", ">");
		AM.colorHexCharmDescriptionStat = StringExtension.Concat("<color=#", "3A2D1DFF", ">");
		AM.colorHexCharmActivationUpgrade = StringExtension.Concat("<color=#", "ABE229FF", ">");
		AM.colorRestBonus = StringExtension.Concat("<color=#", "6c7e00FF", ">");
		AM.colorRestBonusCapped = StringExtension.Concat("<color=#", "6c7e00FF", ">");
	}

	public static float LinearEquationFloat(float a, float b, float c)
	{
		return a * b + c;
	}

	public static double LinearEquationDouble(double a, double b, double c)
	{
		return a * b + c;
	}

	public static int LinearEquationInteger(int a, int b, int c)
	{
		return a * b + c;
	}

	public static string LinearEquationFloatToRoundedString(float a, float b, float c, float roundness = 1f)
	{
		return AM.GetRoundedFloatString(AM.LinearEquationFloat(a, b, c), roundness);
	}

	public static string LinearEquationDoubleToRoundedString(double a, double b, double c, float roundness = 1f)
	{
		return AM.GetRoundedDoubleString(AM.LinearEquationDouble(a, b, c), (double)roundness);
	}

	public static string GetRoundedFloatString(float number, float roundAmount = 1f)
	{
		return GameMath.GetDetailedNumberString((float)Mathf.RoundToInt(number * roundAmount) / roundAmount);
	}

	public static float RoundNumber(float number, float roundAmount = 1000f)
	{
		return (float)Mathf.RoundToInt(number * roundAmount) / roundAmount;
	}

	public static double RoundNumber(double number, double roundAmount = 1000.0)
	{
		return Math.Round(number * roundAmount) / roundAmount;
	}

	public static string GetRoundedDoubleString(double number, double roundAmount = 1.0)
	{
		return GameMath.GetDetailedNumberString(Math.Round(number * roundAmount) / roundAmount);
	}

	public static string cs(string s, Color color)
	{
		string @string = AM.ColorToHex(color);
		return StringExtension.Concat("<color=#", @string, ">", s, "</color>");
	}

	public static string cs(string s, string hexCode)
	{
		return StringExtension.Concat("<color=#", hexCode, ">", s, "</color>");
	}

	public static string csw(string s)
	{
		return StringExtension.Concat(AM.colorHexWhite, s, "</color>");
	}

	public static string csToken(string s)
	{
		return StringExtension.Concat(AM.colorHexToken, s, "</color>");
	}

	public static string csScrap(string s)
	{
		return StringExtension.Concat(AM.colorHexScrap, s, "</color>");
	}

	public static string csGem(string s)
	{
		return StringExtension.Concat(AM.colorHexGem, s, "</color>");
	}

	public static string csCandy(string s)
	{
		return StringExtension.Concat(AM.colorHexCandy, s, "</color>");
	}

	public static string csa(string s)
	{
		return StringExtension.Concat(AM.colorHexAmount, s, "</color>");
	}

	public static string cdu(string s)
	{
		return StringExtension.Concat(AM.colorHexCharmDescriptionUpgrade, s, "</color>");
	}

	public static string cdRest(string s)
	{
		return StringExtension.Concat(AM.colorRestBonus, s, "</color>");
	}

	public static string cdRestCapped(string s)
	{
		return StringExtension.Concat(AM.colorRestBonusCapped, s, "</color>");
	}

	public static string cds(string s)
	{
		return StringExtension.Concat(AM.colorHexCharmDescriptionStat, s, "</color>");
	}

	public static string cas(string s)
	{
		return StringExtension.Concat(AM.colorHexWhite, s, "</color>");
	}

	public static string cau(string s)
	{
		return StringExtension.Concat(AM.colorHexCharmActivationUpgrade, s, "</color>");
	}

	public static string cst(string s)
	{
		return StringExtension.Concat(AM.colorHexTime, s, "</color>");
	}

	public static string csl(string s)
	{
		return StringExtension.Concat(AM.colorHexLevel, s, "</color>");
	}

	public static string csg(string s)
	{
		return StringExtension.Concat(AM.colorHexGreen, s, "</color>");
	}

	public static string csm(string s)
	{
		return StringExtension.Concat(AM.colorHexMerchant, s, "</color>");
	}

	public static string csr(string s)
	{
		return StringExtension.Concat(AM.colorHexRuneBold, s, "</color>");
	}

	public static string csri(string s)
	{
		return StringExtension.Concat(AM.colorHexRuneInc, s, "</color>");
	}

	public static string csart(string s)
	{
		return StringExtension.Concat(AM.colorHexArtifactUpgrade, s, "</color>");
	}

	public static string csdb(string s)
	{
		return StringExtension.Concat(AM.colorHexGenericDarkBrown, s, "</color>");
	}

	public static string ColorToHex(Color32 color)
	{
		return StringExtension.Concat(color.r.ToString("X2"), color.g.ToString("X2"), color.b.ToString("X2"), color.a.ToString("X2"));
	}

	public static string SizeText(object s, int size)
	{
		return StringExtension.Concat("<size=", size.ToString(), ">", s.ToString(), "</size>");
	}

	public static string GetCooldownText(float cd, float cd_dec = -1f)
	{
		string str = string.Empty;
		if (cd_dec > 0f)
		{
			str = AM.csl(StringExtension.Concat(" (-", GameMath.GetTimeInSecondsString(cd_dec), ")"));
		}
		return Environment.NewLine + string.Format(LM.Get("SKILL_COOLDOWN"), AM.cst(GameMath.GetTimeInSecondsString(cd) + str));
	}

	public static string colorHexWhite;

	public static string colorHexGreen;

	public static string colorHexAmount;

	public static string colorHexTime;

	public static string colorHexLevel;

	public static string colorHexMerchant;

	public static string colorHexRuneBold;

	public static string colorHexRuneInc;

	public static string colorHexArtifactUpgrade;

	public static string colorHexToken = StringExtension.Concat("<color=#", "B2D618FF", ">");

	public static string colorHexScrap = StringExtension.Concat("<color=#", "FFC104FF", ">");

	public static string colorHexGem = StringExtension.Concat("<color=#", "2FE5E3FF", ">");

	public static string colorHexCandy = StringExtension.Concat("<color=#", "FF433AFF", ">");

	public static string colorHexGenericDarkBrown;

	public static string colorHexCharmDescriptionUpgrade;

	public static string colorHexCharmDescriptionStat;

	public static string colorHexCharmActivationUpgrade;

	public static string colorRestBonus;

	public static string colorRestBonusCapped;

	private const string c_1 = "<color=#";

	private const string c_2 = ">";

	private const string c_3 = "</color>";

	private const string x_2 = "X2";
}
