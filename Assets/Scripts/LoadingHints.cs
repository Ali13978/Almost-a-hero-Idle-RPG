using System;
using System.Collections.Generic;
using UnityEngine;

public static class LoadingHints
{
	public static List<LoadingHints.Hint> GetHints(int stageReached)
	{
		List<LoadingHints.Hint> list = new List<LoadingHints.Hint>();
		foreach (LoadingHints.Hint item in LoadingHints.hints)
		{
			if (item.IsUnlocked(stageReached))
			{
				list.Add(item);
			}
		}
		return list;
	}

	public static void SetUsedHint(string hint)
	{
		string @string = PlayerPrefs.GetString("HintsShown", string.Empty);
		if (@string == string.Empty)
		{
			PlayerPrefs.SetString("HintsShown", hint);
		}
		else
		{
			PlayerPrefs.SetString("HintsShown", @string + "|" + hint);
		}
	}

	public static List<LoadingHints.Hint> hints = new List<LoadingHints.Hint>
	{
		new LoadingHints.Hint("LOADING_HINT_001", 60, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_002", 0, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_003", 0, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_004", 0, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_005", 0, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_006", 0, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_007", 0, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_011", 5, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_012", 6, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_013", 20, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_014", 80, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_017", 35, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_018", 150, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_019", 120, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_021", 120, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_022", 160, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_023", 160, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_024", 200, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_026", 100, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_027", 140, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_028", 300, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_031", 245, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_033", 6, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_036", 415, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_039", 700, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_040", 700, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_041", 645, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_042", 1000, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_043", 1000, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_044", 1210, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_046", 10, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_047", 3, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_048", 900, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_049", 0, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_050", 900, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_051", 700, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_057", 1500, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_059", 1500, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_060", 100, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_061", 500, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_062", 30, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_063", 0, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_065", 80, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_066", 80, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_067", 600, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_068", 80, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_069", 80, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_070", 80, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_071", 0, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_072", 0, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_073", 400, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_074", 85, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_075", 1000, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_076", 1000, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_077", 50, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_078", 80, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_079", 80, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_080", 80, LoadingHints.HintCategory.Other),
		new LoadingHints.Hint("LOADING_HINT_081", 80, LoadingHints.HintCategory.Other)
	};

	public const string LOADING_HINT_001 = "LOADING_HINT_001";

	public const string LOADING_HINT_002 = "LOADING_HINT_002";

	public const string LOADING_HINT_003 = "LOADING_HINT_003";

	public const string LOADING_HINT_004 = "LOADING_HINT_004";

	public const string LOADING_HINT_005 = "LOADING_HINT_005";

	public const string LOADING_HINT_006 = "LOADING_HINT_006";

	public const string LOADING_HINT_007 = "LOADING_HINT_007";

	public const string LOADING_HINT_008 = "LOADING_HINT_008";

	public const string LOADING_HINT_009 = "LOADING_HINT_009";

	public const string LOADING_HINT_010 = "LOADING_HINT_010";

	public const string LOADING_HINT_011 = "LOADING_HINT_011";

	public const string LOADING_HINT_012 = "LOADING_HINT_012";

	public const string LOADING_HINT_013 = "LOADING_HINT_013";

	public const string LOADING_HINT_014 = "LOADING_HINT_014";

	public const string LOADING_HINT_015 = "LOADING_HINT_015";

	public const string LOADING_HINT_016 = "LOADING_HINT_016";

	public const string LOADING_HINT_017 = "LOADING_HINT_017";

	public const string LOADING_HINT_018 = "LOADING_HINT_018";

	public const string LOADING_HINT_019 = "LOADING_HINT_019";

	public const string LOADING_HINT_020 = "LOADING_HINT_020";

	public const string LOADING_HINT_021 = "LOADING_HINT_021";

	public const string LOADING_HINT_022 = "LOADING_HINT_022";

	public const string LOADING_HINT_023 = "LOADING_HINT_023";

	public const string LOADING_HINT_024 = "LOADING_HINT_024";

	public const string LOADING_HINT_025 = "LOADING_HINT_025";

	public const string LOADING_HINT_026 = "LOADING_HINT_026";

	public const string LOADING_HINT_027 = "LOADING_HINT_027";

	public const string LOADING_HINT_028 = "LOADING_HINT_028";

	public const string LOADING_HINT_031 = "LOADING_HINT_031";

	public const string LOADING_HINT_032 = "LOADING_HINT_032";

	public const string LOADING_HINT_033 = "LOADING_HINT_033";

	public const string LOADING_HINT_034 = "LOADING_HINT_034";

	public const string LOADING_HINT_036 = "LOADING_HINT_036";

	public const string LOADING_HINT_037 = "LOADING_HINT_037";

	public const string LOADING_HINT_038 = "LOADING_HINT_038";

	public const string LOADING_HINT_039 = "LOADING_HINT_039";

	public const string LOADING_HINT_040 = "LOADING_HINT_040";

	public const string LOADING_HINT_041 = "LOADING_HINT_041";

	public const string LOADING_HINT_042 = "LOADING_HINT_042";

	public const string LOADING_HINT_043 = "LOADING_HINT_043";

	public const string LOADING_HINT_044 = "LOADING_HINT_044";

	public const string LOADING_HINT_045 = "LOADING_HINT_045";

	public const string LOADING_HINT_046 = "LOADING_HINT_046";

	public const string LOADING_HINT_047 = "LOADING_HINT_047";

	public const string LOADING_HINT_048 = "LOADING_HINT_048";

	public const string LOADING_HINT_049 = "LOADING_HINT_049";

	public const string LOADING_HINT_050 = "LOADING_HINT_050";

	public const string LOADING_HINT_051 = "LOADING_HINT_051";

	public const string LOADING_HINT_052 = "LOADING_HINT_052";

	public const string LOADING_HINT_053 = "LOADING_HINT_053";

	public const string LOADING_HINT_054 = "LOADING_HINT_054";

	public const string LOADING_HINT_055 = "LOADING_HINT_055";

	public const string LOADING_HINT_056 = "LOADING_HINT_056";

	public const string LOADING_HINT_057 = "LOADING_HINT_057";

	public const string LOADING_HINT_058 = "LOADING_HINT_058";

	public const string LOADING_HINT_059 = "LOADING_HINT_059";

	public const string LOADING_HINT_060 = "LOADING_HINT_060";

	public const string LOADING_HINT_061 = "LOADING_HINT_061";

	public const string LOADING_HINT_062 = "LOADING_HINT_062";

	public const string LOADING_HINT_063 = "LOADING_HINT_063";

	public const string LOADING_HINT_065 = "LOADING_HINT_065";

	public const string LOADING_HINT_066 = "LOADING_HINT_066";

	public const string LOADING_HINT_067 = "LOADING_HINT_067";

	public const string LOADING_HINT_068 = "LOADING_HINT_068";

	public const string LOADING_HINT_069 = "LOADING_HINT_069";

	public const string LOADING_HINT_070 = "LOADING_HINT_070";

	public const string LOADING_HINT_071 = "LOADING_HINT_071";

	public const string LOADING_HINT_072 = "LOADING_HINT_072";

	public const string LOADING_HINT_073 = "LOADING_HINT_073";

	public const string LOADING_HINT_074 = "LOADING_HINT_074";

	public const string LOADING_HINT_075 = "LOADING_HINT_075";

	public const string LOADING_HINT_076 = "LOADING_HINT_076";

	public const string LOADING_HINT_077 = "LOADING_HINT_077";

	public const string LOADING_HINT_078 = "LOADING_HINT_078";

	public const string LOADING_HINT_079 = "LOADING_HINT_079";

	public const string LOADING_HINT_080 = "LOADING_HINT_080";

	public const string LOADING_HINT_081 = "LOADING_HINT_081";

	public const string HintsShownSaveKey = "HintsShown";

	public struct Hint
	{
		public Hint(string locKey, int unlockStage, LoadingHints.HintCategory category = LoadingHints.HintCategory.Other)
		{
			this.locKey = locKey;
			this.unlockStage = unlockStage;
			this.category = category;
		}

		public override string ToString()
		{
			return this.locKey;
		}

		public bool IsUnlocked(int maxStageReached)
		{
			return maxStageReached >= this.unlockStage;
		}

		public string locKey;

		public LoadingHints.HintCategory category;

		private int unlockStage;
	}

	public enum HintLevel
	{
		VeryLow,
		Low,
		Medium,
		High,
		VeryHigh
	}

	public enum HintCategory
	{
		Gameplay,
		Ui,
		Lore,
		Other
	}
}
