using System;
using System.Collections.Generic;
using System.Text;
using Static;
using UnityEngine;
using UnityEngine.Networking;

public static class GameMath
{
	static GameMath()
	{
		int i = 0;
		GameMath.numberSuffixes[i++] = string.Empty;
		GameMath.numberSuffixes[i++] = LM.Get("NUMBER_K");
		GameMath.numberSuffixes[i++] = LM.Get("NUMBER_M");
		GameMath.numberSuffixes[i++] = LM.Get("NUMBER_B");
		GameMath.numberSuffixes[i++] = LM.Get("NUMBER_T");
		for (char c = 'A'; c <= 'Z'; c += '\u0001')
		{
			GameMath.numberSuffixes[i++] = new string(c, 2);
		}
		for (char c2 = 'A'; c2 <= 'Z'; c2 += '\u0001')
		{
			GameMath.numberSuffixes[i++] = new string(c2, 3);
		}
		for (char c3 = 'A'; c3 <= 'Z'; c3 += '\u0001')
		{
			GameMath.numberSuffixes[i++] = new string(new char[]
			{
				c3,
				'4'
			});
		}
		for (char c4 = 'A'; c4 <= 'Z'; c4 += '\u0001')
		{
			GameMath.numberSuffixes[i++] = new string(new char[]
			{
				c4,
				'5'
			});
		}
		while (i < 110)
		{
			GameMath.numberSuffixes[i] = "e" + i * 3;
			i++;
		}
	}

	public static int seedArtifact
	{
		get
		{
			if (GameMath._seedArtifact == 0)
			{
				GameMath._seedArtifact = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
			}
			return GameMath._seedArtifact;
		}
		set
		{
			if (value == 0)
			{
				GameMath._seedArtifact = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
			}
			else
			{
				GameMath._seedArtifact = value;
			}
		}
	}

	public static int seedLootpack
	{
		get
		{
			if (GameMath._seedLootpack == 0)
			{
				GameMath._seedLootpack = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
			}
			return GameMath._seedLootpack;
		}
		set
		{
			if (value == 0)
			{
				GameMath._seedLootpack = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
			}
			else
			{
				GameMath._seedLootpack = value;
			}
		}
	}

	public static int seedTrinket
	{
		get
		{
			if (GameMath._seedTrinket == 0)
			{
				GameMath._seedTrinket = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
			}
			return GameMath._seedTrinket;
		}
		set
		{
			if (value == 0)
			{
				GameMath._seedTrinket = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
			}
			else
			{
				GameMath._seedTrinket = value;
			}
		}
	}

	public static int seedCharmpack
	{
		get
		{
			if (GameMath._seedCharmpack == 0)
			{
				GameMath._seedCharmpack = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
			}
			return GameMath._seedCharmpack;
		}
		set
		{
			if (value == 0)
			{
				GameMath._seedCharmpack = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
			}
			else
			{
				GameMath._seedCharmpack = value;
			}
		}
	}

	public static int seedCharmdraft
	{
		get
		{
			if (GameMath._seedCharmdraft == 0)
			{
				GameMath._seedCharmdraft = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
			}
			return GameMath._seedCharmdraft;
		}
		set
		{
			if (value == 0)
			{
				GameMath._seedCharmdraft = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
			}
			else
			{
				GameMath._seedCharmdraft = value;
			}
		}
	}

	public static int seedCursedGate
	{
		get
		{
			if (GameMath._seedCursedGate == 0)
			{
				GameMath._seedCursedGate = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
			}
			return GameMath._seedCursedGate;
		}
		set
		{
			if (value == 0)
			{
				GameMath._seedCursedGate = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
			}
			else
			{
				GameMath._seedCursedGate = value;
			}
		}
	}

	public static int seedNewCurses
	{
		get
		{
			if (GameMath._seedNewCurses == 0)
			{
				GameMath._seedNewCurses = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
			}
			return GameMath._seedNewCurses;
		}
		set
		{
			if (value == 0)
			{
				GameMath._seedNewCurses = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
			}
			else
			{
				GameMath._seedNewCurses = value;
			}
		}
	}

	public static void InitStrings()
	{
		GameMath.numberSuffixes[1] = LM.Get("NUMBER_K");
		GameMath.numberSuffixes[2] = LM.Get("NUMBER_M");
		GameMath.numberSuffixes[3] = LM.Get("NUMBER_B");
		GameMath.numberSuffixes[4] = LM.Get("NUMBER_T");
	}

	public static double GetRandomDouble(double min, double max, GameMath.RandType randType)
	{
		if (randType == GameMath.RandType.NoSeed)
		{
			return min + (double)UnityEngine.Random.value * (max - min);
		}
		return min + (double)GameMath.AahRandom.GetFloat(randType) * (max - min);
	}

	public static float GetRandomFloat(float min, float max, GameMath.RandType randType)
	{
		if (randType == GameMath.RandType.NoSeed)
		{
			return UnityEngine.Random.Range(min, max);
		}
		return min + GameMath.AahRandom.GetFloat(randType) * (max - min);
	}

	public static int GetRandomInt(int min, int exclusiveMax, GameMath.RandType randType)
	{
		if (randType == GameMath.RandType.NoSeed)
		{
			return UnityEngine.Random.Range(min, exclusiveMax);
		}
		return Mathf.FloorToInt((float)min + GameMath.AahRandom.GetFloat(randType) * (float)(exclusiveMax - min));
	}

	public static T GetRandomListElement<T>(this IList<T> list)
	{
		return list[GameMath.GetRandomInt(0, list.Count, GameMath.RandType.NoSeed)];
	}

	public static T GetRandomListElement<T>(this IList<T> list, GameMath.RandType randType)
	{
		return list[GameMath.GetRandomInt(0, list.Count, randType)];
	}

	public static T GetRandomArrayElement<T>(this T[] array)
	{
		return array[GameMath.GetRandomInt(0, array.Length, GameMath.RandType.NoSeed)];
	}

	public static Vector2 GetRandomPointInUnitCircle()
	{
		return UnityEngine.Random.insideUnitCircle;
	}

	public static bool GetProbabilityOutcome(float probability, GameMath.RandType randType)
	{
		if (randType == GameMath.RandType.NoSeed)
		{
			return UnityEngine.Random.value < probability;
		}
		return GameMath.AahRandom.GetFloat(randType) < probability;
	}

	public static int GetRouletteOutcome(List<float> weights, GameMath.RandType randType)
	{
		float num = 0f;
		foreach (float num2 in weights)
		{
			num += num2;
		}
		float num3 = num * ((randType != GameMath.RandType.NoSeed) ? GameMath.AahRandom.GetFloat(randType) : UnityEngine.Random.value);
		float num4 = 0f;
		int count = weights.Count;
		for (int i = 0; i < count; i++)
		{
			num4 += weights[i];
			if (num3 < num4)
			{
				return i;
			}
		}
		for (int j = 0; j < count; j++)
		{
			if ((double)weights[j] != 0.0)
			{
				return j;
			}
		}
		return UnityEngine.Random.Range(0, count);
	}

	public static int[] GetRouletteOutcome(List<float> weights, int count, GameMath.RandType randType)
	{
		int[] array = new int[count];
		for (int i = 0; i < count; i++)
		{
			array[i] = GameMath.GetRouletteOutcome(weights, randType);
		}
		return array;
	}

	public static float PowFloat(float x, float p)
	{
		return (float)Math.Pow((double)x, (double)p);
	}

	public static double PowDouble(double x, double p)
	{
		return Math.Pow(x, p);
	}

	public static double PowInt(double x, int p)
	{
		double num = 1.0;
		while (p > 0)
		{
			if (p % 2 == 1)
			{
				num *= x;
			}
			p /= 2;
			x *= x;
		}
		return num;
	}

	public static int GetMaxInt(int a, int b)
	{
		return (a <= b) ? b : a;
	}

	public static int GetMinInt(int a, int b)
	{
		return (a >= b) ? b : a;
	}

	public static float Sin(float rad)
	{
		return Mathf.Sin(rad);
	}

	public static float Cos(float rad)
	{
		return Mathf.Cos(rad);
	}

	public static float GetMaxFloat(float a, float b)
	{
		return (a <= b) ? b : a;
	}

	public static float GetMinFloat(float a, float b)
	{
		return (a >= b) ? b : a;
	}

	public static double GetMaxDouble(double a, double b)
	{
		return (a <= b) ? b : a;
	}

	public static double GetMinDouble(double a, double b)
	{
		return (a >= b) ? b : a;
	}

	public static double FloorDouble(double x)
	{
		return Math.Floor(x);
	}

	public static double Round(double x)
	{
		return Math.Round(x);
	}

	public static int RoundDoubleToInt(double x)
	{
		return (int)Math.Floor(x);
	}

	public static int CeilToInt(float f)
	{
		return Mathf.CeilToInt(f);
	}

	public static int RoundToInt(float f)
	{
		return Mathf.RoundToInt(f);
	}

	public static int RoundToInt(double f)
	{
		return (int)Math.Round(f);
	}

	public static int Clamp(int value, int min, int max)
	{
		return (value <= max) ? ((value >= min) ? value : min) : max;
	}

	public static float Clamp(float value, float min, float max)
	{
		return Mathf.Clamp(value, min, max);
	}

	public static double Clamp(double value, double min, double max)
	{
		return (value <= max) ? ((value >= min) ? value : min) : max;
	}

	public static float Clamp01(float value)
	{
		return Mathf.Clamp01(value);
	}

	public static double Clamp01(double value)
	{
		return (value <= 1.0) ? ((value >= 0.0) ? value : 0.0) : 1.0;
	}

	public static float D2xy(Vector3 a, Vector3 b)
	{
		float num = a.x - b.x;
		float num2 = a.y - b.y;
		return num * num + num2 * num2;
	}

	public static float Dxy(Vector3 a, Vector3 b)
	{
		return Mathf.Sqrt(GameMath.D2xy(a, b));
	}

	public static bool AreInsideRangeXY(Vector3 a, Vector3 b, float r2)
	{
		return GameMath.D2xy(a, b) <= r2;
	}

	public static float D2(Vector3 a, Vector3 b)
	{
		return (b - a).sqrMagnitude;
	}

	public static float D(Vector3 a, Vector3 b)
	{
		return Mathf.Sqrt(GameMath.D2(a, b));
	}

	public static Vector3 Lerp(Vector3 a, Vector3 b, float ratio)
	{
		return Vector3.LerpUnclamped(a, b, ratio);
	}

	public static float Lerp(float a, float b, float ratio)
	{
		return Mathf.Lerp(a, b, ratio);
	}

	public static double Lerp(double a, double b, double ratio)
	{
		return a + (b - a) * ratio;
	}

	public static DateTime GetNow()
	{
		return DateTime.Now;
	}

	public static TimeSpan DeltaTime(DateTime time0, DateTime time1)
	{
		return time0.Subtract(time1);
	}

	public static double DeltaTimeInSecs(DateTime time0, DateTime time1)
	{
		return GameMath.DeltaTime(time0, time1).TotalSeconds;
	}

	public static Vector3 ConvertToScreenSpace(Vector3 simPos)
	{
		Vector3 result = simPos;
		result.z = GameMath.ConvertToScreenSpaceZ(result.y);
		return result;
	}

	public static float ConvertToScreenSpaceZ(float y)
	{
		return y / 100f;
	}

	public static Vector3 ConvertPointerPosToGameSpace(Vector3 pointerPos)
	{
		return Camera.main.ScreenToWorldPoint(pointerPos);
	}

	public static string GetFlooredDoubleString(double x)
	{
		if (x < 1000.0)
		{
			return GameMath.GetDoubleString(x);
		}
		int num = Mathf.FloorToInt((float)Math.Log10(x)) - 2;
		double num2 = Math.Pow(10.0, (double)num);
		double x2 = Math.Floor(x / num2) * num2;
		return GameMath.GetDoubleString(x2);
	}

	public static StringBuilder GetFlooredDoubleString(double x, StringBuilder stringBuilder)
	{
		if (x < 1000.0)
		{
			return GameMath.GetDoubleString(x, stringBuilder);
		}
		int num = Mathf.FloorToInt((float)Math.Log10(x)) - 2;
		double num2 = Math.Pow(10.0, (double)num);
		double x2 = Math.Floor(x / num2) * num2;
		return GameMath.GetDoubleString(x2, stringBuilder);
	}

	public static string GetDoubleString(double x)
	{
		return GameMath.GetDoubleString(x, StringExtension.StringBuilder).ToString();
	}

	public static StringBuilder GetDoubleString(double x, StringBuilder stringBuilder)
	{
		if (double.IsInfinity(x))
		{
			return stringBuilder.Append("INF");
		}
		if (double.IsNaN(x))
		{
			return stringBuilder.Append("NaN");
		}
		if (x < 0.0)
		{
			if (-1.0 < x)
			{
				return stringBuilder.Append("0");
			}
			stringBuilder.Append("-");
			double num = x - 1E-05;
			int num2 = (int)x;
			if (x > -1000.0 && (int)num < num2)
			{
				x = num;
			}
			x = -x;
		}
		if (x < 1000.0)
		{
			stringBuilder.Append((int)x);
			return stringBuilder;
		}
		GameMath.NotationStyle notation_STYLE = GameMath.NOTATION_STYLE;
		if (notation_STYLE == GameMath.NotationStyle.CLASSIC)
		{
			GameMath.ClassicNotation(x, stringBuilder);
			return stringBuilder;
		}
		if (notation_STYLE != GameMath.NotationStyle.SCIENTIFIC)
		{
			throw new Exception(GameMath.NOTATION_STYLE.ToString());
		}
		if (x < 1000.0)
		{
			GameMath.ClassicNotation(x, stringBuilder);
			return stringBuilder;
		}
		int num3 = Mathf.FloorToInt((float)Math.Log10(x));
		double num4 = x;
		for (int i = 0; i < num3; i++)
		{
			num4 /= 10.0;
		}
		return stringBuilder.Append(num4.ToString("F2")).Append("E").Append(num3);
	}

	private static void ClassicNotation(double x, StringBuilder stringBuilder)
	{
		int num = 0;
		while (x >= 1000.0)
		{
			x /= 1000.0;
			num++;
		}
		if (x >= 100.0)
		{
			stringBuilder.Append((int)x);
		}
		else if (x >= 10.0)
		{
			stringBuilder.Append(x.ToString("F1"));
		}
		else
		{
			stringBuilder.Append(x.ToString("F2"));
		}
		stringBuilder.Append(GameMath.numberSuffixes[num]);
	}

	public static string GetDetailedNumberString(double x)
	{
		if (double.IsInfinity(x))
		{
			return "INF";
		}
		if (double.IsNaN(x))
		{
			return "NaN";
		}
		if (x < 0.0)
		{
			return "-" + GameMath.GetDetailedNumberString(-x);
		}
		if (x >= 1000.0)
		{
			return GameMath.GetDoubleString(x);
		}
		if (x >= 100.0)
		{
			return x.ToString("F0");
		}
		if (x >= 10.0)
		{
			string text = x.ToString("F1");
			if (text[text.Length - 1] == '0')
			{
				return x.ToString("F0");
			}
			return text;
		}
		else
		{
			string text2 = x.ToString("F2");
			if (text2.Substring(text2.Length - 2, 2) == "00")
			{
				return x.ToString("F0");
			}
			if (text2[text2.Length - 1] == '0')
			{
				return x.ToString("F1");
			}
			return text2;
		}
	}

	public static string GetDetailedNumberString(float x)
	{
		if (float.IsInfinity(x))
		{
			return "INF";
		}
		if (float.IsNaN(x))
		{
			return "NaN";
		}
		if (x < 0f)
		{
			return "-" + GameMath.GetDetailedNumberString(-x);
		}
		if (x >= 100f)
		{
			return x.ToString("F0");
		}
		if (x >= 10f)
		{
			string text = x.ToString("F1");
			if (text[text.Length - 1] == '0')
			{
				return x.ToString("F0");
			}
			return text;
		}
		else
		{
			string text2 = x.ToString("F2");
			if (text2.Substring(text2.Length - 2, 2) == "00")
			{
				return x.ToString("F0");
			}
			if (text2[text2.Length - 1] == '0')
			{
				return x.ToString("F1");
			}
			return text2;
		}
	}

	public static string GetTimeString(float timeInSeconds)
	{
		return GameMath.GetTimeString((int)Mathf.Floor(timeInSeconds));
	}

	public static string GetTimeString(double timeInSeconds)
	{
		return GameMath.GetTimeString((int)Math.Floor(timeInSeconds));
	}

	public static string GetTimeString(int timeInSeconds)
	{
		return GameMath.GetTimeString(timeInSeconds, StringExtension.StringBuilder).ToString();
	}

	public static StringBuilder GetTimeString(float timeInSeconds, StringBuilder stringBuilder)
	{
		return GameMath.GetTimeString((int)Mathf.Floor(timeInSeconds), stringBuilder);
	}

	public static StringBuilder GetTimeString(double timeInSeconds, StringBuilder stringBuilder)
	{
		return GameMath.GetTimeString((int)Math.Floor(timeInSeconds), stringBuilder);
	}

	public static StringBuilder GetTimeString(int timeInSeconds, StringBuilder stringBuilder)
	{
		int num = timeInSeconds / 3600;
		int num2 = timeInSeconds / 60 % 60;
		int num3 = timeInSeconds % 60;
		stringBuilder.Length = 0;
		if (num > 0)
		{
			stringBuilder.Append(num);
			stringBuilder.Append(":");
			if (num2 < 10)
			{
				stringBuilder.Append("0");
			}
			if (num2 == 0)
			{
				stringBuilder.Append("0");
				stringBuilder.Append(":");
			}
		}
		if (num2 > 0)
		{
			stringBuilder.Append(num2);
			stringBuilder.Append(":");
			if (num3 < 10)
			{
				stringBuilder.Append("0");
			}
		}
		stringBuilder.Append(num3);
		return stringBuilder;
	}

	public static string GetLocalizedTimeString(TimeSpan time)
	{
		StringBuilder stringBuilder = StringExtension.StringBuilder;
		return GameMath.GetLocalizedTimeString(time.Days, time.Hours, time.Minutes, time.Seconds, stringBuilder).ToString();
	}

	public static StringBuilder GetLocalizedTimeString(float seconds, StringBuilder stringBuilder)
	{
		int days = (int)(seconds / 86400f);
		seconds = (float)((int)(seconds % 86400f));
		int hours = (int)(seconds / 3600f);
		seconds = (float)((int)(seconds % 3600f));
		int minutes = (int)(seconds / 60f);
		seconds %= 60f;
		return GameMath.GetLocalizedTimeString(days, hours, minutes, (int)seconds, stringBuilder);
	}

	public static StringBuilder GetLocalizedTimeString(int days, int hours, int minutes, int seconds, StringBuilder stringBuilder)
	{
		if (days > 0)
		{
			return stringBuilder.Append(string.Format(LM.Get("DATETIME_DAYS"), days)).Append(" ").Append(string.Format(LM.Get("DATETIME_HOURS"), hours.ToString("00"))).Append(" ").Append(string.Format(LM.Get("DATETIME_MINUTES"), minutes.ToString("00"))).Append(" ").Append(string.Format(LM.Get("DATETIME_SECONDS"), seconds.ToString("00")));
		}
		if (hours > 0)
		{
			return stringBuilder.Append(string.Format(LM.Get("DATETIME_HOURS"), hours)).Append(" ").Append(string.Format(LM.Get("DATETIME_MINUTES"), minutes.ToString("00"))).Append(" ").Append(string.Format(LM.Get("DATETIME_SECONDS"), seconds.ToString("00")));
		}
		if (minutes > 0)
		{
			return stringBuilder.Append(string.Format(LM.Get("DATETIME_MINUTES"), minutes.ToString())).Append(" ").Append(string.Format(LM.Get("DATETIME_SECONDS"), seconds.ToString("00")));
		}
		return stringBuilder.Append(string.Format(LM.Get("DATETIME_SECONDS"), seconds));
	}

	public static string GetTimeString(TimeSpan time)
	{
		if (time.Days > 1)
		{
			return string.Format(LM.Get("DAY_AND_TIME_PLURAL"), time.Days.ToString(), string.Concat(new object[]
			{
				time.Hours,
				":",
				time.Minutes.ToString("00"),
				":",
				time.Seconds.ToString("00")
			}));
		}
		if (time.Days > 0)
		{
			return string.Format(LM.Get("DAY_AND_TIME"), time.Days.ToString(), string.Concat(new object[]
			{
				time.Hours,
				":",
				time.Minutes.ToString("00"),
				":",
				time.Seconds.ToString("00")
			}));
		}
		if (time.Hours > 0)
		{
			return string.Concat(new object[]
			{
				time.Hours,
				":",
				time.Minutes.ToString("00"),
				":",
				time.Seconds.ToString("00")
			});
		}
		if (time.Minutes > 0)
		{
			return time.Minutes.ToString() + ":" + time.Seconds.ToString("00");
		}
		return time.Seconds.ToString() + LM.Get("TIME_SEC_SHORT");
	}

	public static string GetTimeDatailedShortenedString(TimeSpan time)
	{
		string result;
		if (time.Days > 0)
		{
			result = string.Format(LM.Get("DATETIME_DAYS"), time.Days) + " " + string.Format(LM.Get("DATETIME_HOURS"), time.Hours);
		}
		else if (time.Hours > 0)
		{
			result = string.Format(LM.Get("DATETIME_HOURS"), time.Hours) + " " + string.Format(LM.Get("DATETIME_MINUTES"), time.Minutes);
		}
		else if (time.Minutes > 0)
		{
			result = string.Format(LM.Get("DATETIME_MINUTES"), time.Minutes) + " " + string.Format(LM.Get("DATETIME_SECONDS"), time.Seconds);
		}
		else
		{
			result = string.Format(LM.Get("DATETIME_SECONDS"), time.Seconds);
		}
		return result;
	}

	public static string GetTimeRecursive(TimeSpan time)
	{
		StringBuilder stringBuilder = new StringBuilder();
		CustomTimeSpan customTimeSpan = new CustomTimeSpan(time);
		int num = 0;
		bool flag = customTimeSpan.Years > 0;
		bool flag2 = customTimeSpan.Days > 0;
		bool flag3 = customTimeSpan.Hours > 0;
		if (flag)
		{
			num++;
		}
		if (flag2)
		{
			num++;
		}
		if (flag3)
		{
			num++;
		}
		bool flag4 = customTimeSpan.Minutes > 0 && num < 3;
		if (flag4)
		{
			num++;
		}
		bool flag5 = customTimeSpan.Seconds > 0 && num < 3;
		if (flag)
		{
			stringBuilder.AppendFormat(LM.Get("DATETIME_YEARS"), customTimeSpan.Years);
			if (flag2 || flag3 || flag4 || flag5)
			{
				stringBuilder.Append(' ');
			}
		}
		if (flag2)
		{
			stringBuilder.AppendFormat(LM.Get("DATETIME_DAYS", customTimeSpan.Days), new object[0]);
			if (flag3 || flag4 || flag5)
			{
				stringBuilder.Append(' ');
			}
		}
		if (flag3)
		{
			stringBuilder.AppendFormat(LM.Get("DATETIME_HOURS", customTimeSpan.Hours), new object[0]);
			if (flag4 || flag5)
			{
				stringBuilder.Append(' ');
			}
		}
		if (flag4)
		{
			stringBuilder.AppendFormat(LM.Get("DATETIME_MINUTES", customTimeSpan.Minutes), new object[0]);
			if (flag5)
			{
				stringBuilder.Append(' ');
			}
		}
		if (flag5)
		{
			stringBuilder.AppendFormat(LM.Get("DATETIME_SECONDS"), customTimeSpan.Seconds);
		}
		return stringBuilder.ToString();
	}

	public static string GetTimeForSpecialOffers(TimeSpan time)
	{
		string result;
		if (time.Days > 1)
		{
			result = string.Format(LM.Get("DAY_AND_TIME_PLURAL"), time.Days, string.Empty);
		}
		else if (time.Days > 0)
		{
			result = string.Format(LM.Get("DAY_AND_TIME"), time.Days, string.Empty);
		}
		else if (time.Hours > 0)
		{
			result = string.Format("{0} " + LM.Get("TIME_HOURS"), time.Hours);
		}
		else if (time.Minutes > 0)
		{
			result = string.Format("{0} " + LM.Get("TIME_MINUTES"), time.Minutes);
		}
		else
		{
			result = string.Format(LM.Get("DATETIME_SECONDS"), time.Seconds);
		}
		return result;
	}

	public static string GetTimeLessDetailedString(double timeInSeconds, bool capsOn = true)
	{
		StringBuilder stringBuilder = StringExtension.StringBuilder;
		return GameMath.GetTimeLessDetailedString(timeInSeconds, stringBuilder, capsOn).ToString();
	}

	public static StringBuilder GetTimeLessDetailedString(double timeInSeconds, StringBuilder stringBuilder, bool capsOn = true)
	{
		int num = GameMath.RoundDoubleToInt(GameMath.FloorDouble(timeInSeconds));
		float num2 = Mathf.Floor((float)(num / 3600));
		if (num2 > 0f)
		{
			return stringBuilder.Append(num2.ToString()).Append(" ").Append((!capsOn) ? LM.Get("TIME_HR") : LM.Get("TIME_HOURS"));
		}
		float num3 = Mathf.Floor((float)(num / 60 % 60));
		if (num3 > 0f)
		{
			return stringBuilder.Append(num3.ToString()).Append(" ").Append((!capsOn) ? LM.Get("TIME_MIN") : LM.Get("TIME_MINUTES"));
		}
		return stringBuilder.Append(((float)(num % 60)).ToString()).Append(" ").Append((!capsOn) ? LM.Get("TIME_SEC") : LM.Get("TIME_SECONDS"));
	}

	public static string GetTimeDetailedString(double timeInSeconds, bool capsOn = true)
	{
		int num = GameMath.RoundDoubleToInt(GameMath.FloorDouble(timeInSeconds));
		float num2 = Mathf.Floor((float)(num / 3600));
		string text = string.Empty;
		if (num2 > 0f)
		{
			text = text + num2.ToString() + " " + ((!capsOn) ? LM.Get("TIME_HR") : LM.Get("TIME_HOURS"));
		}
		float num3 = Mathf.Floor((float)(num / 60 % 60));
		if (num3 > 0f)
		{
			if (text != string.Empty)
			{
				text += " ";
			}
			text = text + num3.ToString() + " " + ((!capsOn) ? LM.Get("TIME_MIN") : LM.Get("TIME_MINUTES"));
		}
		float num4 = (float)(num % 60);
		if (num4 > 0f)
		{
			if (text != string.Empty)
			{
				text += " ";
			}
			text = text + num4.ToString() + " " + ((!capsOn) ? LM.Get("TIME_SEC") : LM.Get("TIME_SECONDS"));
		}
		return text;
	}

	public static string GetTimeInSecondsString(float timeInSeconds)
	{
		return StringExtension.Concat(((int)Mathf.Floor(timeInSeconds)).ToString(), LM.Get("TIME_SEC_SHORT"));
	}

	public static string GetTimeInMilliSecondsString(float timeInSeconds)
	{
		string text = string.Empty;
		if (timeInSeconds < 0f)
		{
			timeInSeconds = -timeInSeconds;
			text = "-";
		}
		int num = (int)Mathf.Floor(timeInSeconds);
		float num2 = (timeInSeconds - (float)num) * 100f;
		if (num2 >= 10f)
		{
			return string.Concat(new string[]
			{
				text,
				num.ToString(),
				".",
				(num2 / 10f).ToString("0"),
				LM.Get("TIME_SEC_SHORT")
			});
		}
		if (num2 >= 1f)
		{
			return string.Concat(new string[]
			{
				text,
				num.ToString(),
				".",
				num2.ToString("00"),
				LM.Get("TIME_SEC_SHORT")
			});
		}
		return text + num.ToString() + LM.Get("TIME_SEC_SHORT");
	}

	public static string GetPercentString(float number, bool round = false)
	{
		if (round)
		{
			return string.Format(LM.Get("PERCENT"), GameMath.GetDoubleString((double)(number * 100f)));
		}
		return string.Format(LM.Get("PERCENT"), GameMath.GetDetailedNumberString((float)Mathf.RoundToInt(number * 10000f) / 100f));
	}

	public static string GetPercentString(double number, bool round = false)
	{
		if (round)
		{
			return string.Format(LM.Get("PERCENT"), GameMath.GetDoubleString(number * 100.0));
		}
		return string.Format(LM.Get("PERCENT"), GameMath.GetDetailedNumberString(number * 100.0));
	}

	public static string GetColorString(Color c)
	{
		return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", new object[]
		{
			GameMath.ToByte(c.r),
			GameMath.ToByte(c.g),
			GameMath.ToByte(c.b),
			GameMath.ToByte(c.a)
		});
	}

	private static byte ToByte(float f)
	{
		f = Mathf.Clamp01(f);
		return (byte)(f * 255f);
	}

	public static string ClearUrl(string url)
	{
		return UnityWebRequest.EscapeURL(url).Replace("+", "%20");
	}

	public static GameMath.VersionComparisonResult CompareVersions(string current, string toCompare)
	{
		string[] array = current.Split(new char[]
		{
			'.'
		});
		string[] array2 = toCompare.Split(new char[]
		{
			'.'
		});
		if (array.Length != array2.Length)
		{
			return GameMath.VersionComparisonResult.NON_COMPARABLE;
		}
		for (int i = 0; i < array.Length; i++)
		{
			int num = Convert.ToInt32(array[i]);
			int num2 = Convert.ToInt32(array2[i]);
			if (num != num2)
			{
				if (num > num2)
				{
					return GameMath.VersionComparisonResult.NEW;
				}
				if (num < num2)
				{
					return GameMath.VersionComparisonResult.OLD;
				}
			}
		}
		return GameMath.VersionComparisonResult.EQUAL;
	}

	public static int FloorToInt(float v)
	{
		return Mathf.FloorToInt(v);
	}

	public static Vector3 RectViewportToWorldPosition(this RectTransform rectTransform, Vector2 viewPos)
	{
		Vector3[] array = new Vector3[4];
		rectTransform.GetWorldCorners(array);
		Vector3 a = array[1];
		Vector3 b = array[2];
		Vector3 a2 = array[0];
		Vector3 b2 = array[3];
		Vector3 b3 = Vector3.LerpUnclamped(a, b, viewPos.x);
		Vector3 a3 = Vector3.LerpUnclamped(a2, b2, viewPos.x);
		return Vector3.LerpUnclamped(a3, b3, viewPos.y);
	}

	public static bool SigmaEqual(this float x, float y)
	{
		return Mathf.Abs(x - y) <= 0.001f;
	}

	public static bool SigmaEqual(this int x, int y)
	{
		return x == y;
	}

	public static bool SigmaEqual(this double x, double y)
	{
		return Math.Abs(x - y) <= 0.001;
	}

	public static double GetTotalDiscountFactor(double baseDiscountPercent, int timesApplied)
	{
		return GameMath.PowDouble(1.0 - baseDiscountPercent, (double)timesApplied);
	}

	public static float GetTotalDiscountFactor(float baseDiscountPercent, int timesApplied)
	{
		return GameMath.PowFloat(1f - baseDiscountPercent, (float)timesApplied);
	}

	public static double GetTotalIncreaseFactor(double baseIncreasePercent, int timesApplied)
	{
		return GameMath.PowDouble(1.0 + baseIncreasePercent, (double)timesApplied);
	}

	public static float GetTotalIncreaseFactor(float baseIncreasePercent, int timesApplied)
	{
		return GameMath.PowFloat(1f + baseIncreasePercent, (float)timesApplied);
	}

	public static int NumAppearencesOf<T>(this T[] array, T element)
	{
		int num = 0;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].Equals(element))
			{
				num++;
			}
		}
		return num;
	}

	public static int NumAppearencesOf<T>(this List<T> array, T element)
	{
		int num = 0;
		for (int i = 0; i < array.Count; i++)
		{
			T t = array[i];
			if (t.Equals(element))
			{
				num++;
			}
		}
		return num;
	}

	public static GameMath.NotationStyle NOTATION_STYLE;

	public const double SCIENTIFIC_NOTATION_THRESHOLD = 1000.0;

	private static readonly string[] numberSuffixes = new string[110];

	private static int _seedArtifact;

	private static int _seedLootpack;

	private static int _seedTrinket;

	private static int _seedCharmpack;

	private static int _seedCharmdraft;

	private static int _seedCursedGate;

	private static int _seedNewCurses;

	public enum NotationStyle
	{
		CLASSIC,
		SCIENTIFIC
	}

	public enum RandType
	{
		NoSeed,
		Artifact,
		Lootpack,
		Trinket,
		CharmPack,
		CharmDraft,
		CursedGate,
		NewCurses
	}

	public enum VersionComparisonResult
	{
		EQUAL,
		OLD,
		NEW,
		NON_COMPARABLE
	}

	public static class AahRandom
	{
		public static float GetFloat(GameMath.RandType rt)
		{
			return Mathf.Abs(1f * (float)GameMath.AahRandom.GetInt(rt) / 2.14748365E+09f);
		}

		public static int GetInt(GameMath.RandType rt)
		{
			if (rt == GameMath.RandType.Artifact)
			{
				GameMath.seedArtifact = GameMath.AahRandom.GetInt0(GameMath.seedArtifact);
				return GameMath.seedArtifact;
			}
			if (rt == GameMath.RandType.Lootpack)
			{
				GameMath.seedLootpack = GameMath.AahRandom.GetInt1(GameMath.seedLootpack);
				return GameMath.seedLootpack;
			}
			if (rt == GameMath.RandType.Trinket)
			{
				GameMath.seedTrinket = GameMath.AahRandom.GetInt2(GameMath.seedTrinket);
				return GameMath.seedTrinket;
			}
			if (rt == GameMath.RandType.CharmPack)
			{
				GameMath.seedCharmpack = GameMath.AahRandom.GetInt3(GameMath.seedCharmpack);
				return GameMath.seedCharmpack;
			}
			if (rt == GameMath.RandType.CharmDraft)
			{
				GameMath.seedCharmdraft = GameMath.AahRandom.GetInt4(GameMath.seedCharmdraft);
				return GameMath.seedCharmdraft;
			}
			if (rt == GameMath.RandType.CursedGate)
			{
				GameMath.seedCursedGate = GameMath.AahRandom.GetInt4(GameMath.seedCursedGate);
				return GameMath.seedCursedGate;
			}
			if (rt == GameMath.RandType.NewCurses)
			{
				GameMath.seedNewCurses = GameMath.AahRandom.GetInt4(GameMath.seedNewCurses);
				return GameMath.seedNewCurses;
			}
			throw new NotImplementedException();
		}

		private static int GetInt0(int seed)
		{
			return (1664525 * seed + 1013904223) % int.MaxValue;
		}

		private static int GetInt1(int seed)
		{
			return (22695477 * seed + 1) % int.MaxValue;
		}

		private static int GetInt2(int seed)
		{
			return (1103515245 * seed + 12345) % int.MaxValue;
		}

		private static int GetInt3(int seed)
		{
			return (134775813 * seed + 1) % int.MaxValue;
		}

		private static int GetInt4(int seed)
		{
			return 48271 * seed % int.MaxValue;
		}

		private const int a0 = 1664525;

		private const int c0 = 1013904223;

		private const int a1 = 22695477;

		private const int c1 = 1;

		private const int a2 = 1103515245;

		private const int c2 = 12345;

		private const int a3 = 134775813;

		private const int c3 = 1;

		private const int a4 = 48271;

		private const int c4 = 0;

		private const int m = 2147483647;
	}
}
