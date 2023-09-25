using System;
using System.Collections.Generic;

namespace Simulation
{
	public class CharmDataBase : EnchantmentDataBase
	{
		static CharmDataBase()
		{
			CharmDataBase.WeightIncreasers = new List<float>();
			CharmDataBase.AddCharmPackData(IndexRange.Create(5, 5), 88f, 1f);
			CharmDataBase.AddCharmPackData(IndexRange.Create(20, 20), 10f, 1.2f);
			CharmDataBase.AddCharmPackData(IndexRange.Create(50, 50), 2f, 1.2f);
		}

		private static void AddCharmPackData(IndexRange range, float weight, float extraWeight)
		{
			CharmDataBase.CharmPackRarityDupeCounts.Add(range);
			CharmDataBase.CharmPackBaseRarityTable.Add(weight);
			CharmDataBase.WeightIncreasers.Add(extraWeight);
		}

		public static int GetPackCounter(int index)
		{
			int num = index - CharmDataBase.NotOpenedCounters.Count + 1;
			for (int i = 0; i < num; i++)
			{
				CharmDataBase.NotOpenedCounters.Add(0);
			}
			return CharmDataBase.NotOpenedCounters[index];
		}

		public static void IncrementPackCounter(int index)
		{
			int num = index - CharmDataBase.NotOpenedCounters.Count + 1;
			for (int i = 0; i < num; i++)
			{
				CharmDataBase.NotOpenedCounters.Add(0);
			}
			List<int> notOpenedCounters;
			(notOpenedCounters = CharmDataBase.NotOpenedCounters)[index] = notOpenedCounters[index] + 1;
		}

		public static void ResetPackCounter(int index)
		{
			int num = index - CharmDataBase.NotOpenedCounters.Count + 1;
			for (int i = 0; i < num; i++)
			{
				CharmDataBase.NotOpenedCounters.Add(0);
			}
			CharmDataBase.NotOpenedCounters[index] = 0;
		}

		public int maxLevel;

		public float dropWeight;

		public bool isAlwaysActive;

		public CharmType charmType;

		public static readonly List<float> CharmPackBaseRarityTable = new List<float>();

		public static readonly List<IndexRange> CharmPackRarityDupeCounts = new List<IndexRange>();

		public static readonly List<float> WeightIncreasers;

		public static List<int> NotOpenedCounters = new List<int>();
	}
}
