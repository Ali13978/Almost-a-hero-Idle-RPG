using System;
using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
	public abstract class TrinketEffect
	{
		public abstract string GetDebugName();

		public abstract string GetDesc(bool withUpgrade, int lev = -1);

		public abstract string GetDescFirstWithoutColor();

		public void Apply(Hero hero, Dictionary<int, int> oldGenericCounters, Dictionary<int, float> oldGenericTimers, Dictionary<int, bool> oldGenericFlags)
		{
			TrinketEffect.buffDataList.Clear();
			this.InitBuffData(ref TrinketEffect.buffDataList);
			int i = 0;
			int count = TrinketEffect.buffDataList.Count;
			while (i < count)
			{
				BuffData buffData = TrinketEffect.buffDataList[i];
				int genericCounter = (!oldGenericCounters.ContainsKey(buffData.id)) ? 0 : oldGenericCounters[buffData.id];
				buffData.genericTimer = ((!oldGenericTimers.ContainsKey(buffData.id)) ? 0f : oldGenericTimers[buffData.id]);
				buffData.genericFlag = (oldGenericFlags.ContainsKey(buffData.id) && oldGenericFlags[buffData.id]);
				hero.AddBuff(buffData, genericCounter, false);
				i++;
			}
		}

		protected abstract void InitBuffData(ref List<BuffData> buffDataList);

		public abstract float GetChanceWeight();

		public TrinketEffect GetCopy()
		{
			TrinketEffect trinketEffect = (TrinketEffect)Activator.CreateInstance(base.GetType());
			trinketEffect.level = 0;
			return trinketEffect;
		}

		public abstract TrinketEffectGroup GetGroup();

		public abstract int GetRarity();

		public abstract int GetMaxLevel();

		public abstract Sprite[] GetSprites();

		public string csg(string str)
		{
			return AM.csr(str);
		}

		public bool EffectGroupPredicate(TrinketEffect other)
		{
			return this.GetGroup() == other.GetGroup();
		}

		public const int LEVEL_COMMON = 0;

		public const int LEVEL_UNCOMMON = 1;

		public const int LEVEL_RARE = 2;

		public const int LEVEL_EPIC = 3;

		public const int LEVEL_LEGENDARY = 4;

		public const int LEVEL_MYTHICAL = 5;

		public static TrinketEffectSorter effectSorter = new TrinketEffectSorter();

		protected static List<BuffData> buffDataList = new List<BuffData>();

		public int level;
	}
}
