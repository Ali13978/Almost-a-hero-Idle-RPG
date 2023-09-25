using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectMissChance : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataTEMissChance buffDataTEMissChance = new BuffDataTEMissChance(this.GetChance(this.level));
			buffDataTEMissChance.id = 209;
			buffDataTEMissChance.isPermenant = true;
			buffDataList.Add(buffDataTEMissChance);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.01f + (float)lev * 0.01f, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.01f, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_MISS_CHANCE"), text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.01f, false);
			return string.Format(LM.Get("TRINKET_EFFECT_MISS_CHANCE"), percentString);
		}

		private float GetChance(int level)
		{
			if (level == TrinketEffectMissChance.levelLast)
			{
				return TrinketEffectMissChance.chanceLast;
			}
			TrinketEffectMissChance.levelLast = level;
			TrinketEffectMissChance.chanceLast = 0.01f;
			for (int i = 0; i < level; i++)
			{
				TrinketEffectMissChance.chanceLast += (1f - TrinketEffectMissChance.chanceLast) * 0.01f;
			}
			return TrinketEffectMissChance.chanceLast;
		}

		public override float GetChanceWeight()
		{
			return 1f;
		}

		public override int GetRarity()
		{
			return 2;
		}

		public override Sprite[] GetSprites()
		{
			return new Sprite[]
			{
				UiData.inst.spriteTrinketBeads[0],
				UiData.inst.spriteTrinketBeads[0],
				UiData.inst.spriteTrinketBeads[0]
			};
		}

		public override TrinketEffectGroup GetGroup()
		{
			return TrinketEffectGroup.COMMON;
		}

		public override int GetMaxLevel()
		{
			return 10;
		}

		public override string GetDebugName()
		{
			return "Miss Chance";
		}

		public const float CHANCE_BASE = 0.01f;

		public const float CHANCE_LEVEL = 0.01f;

		private static int levelLast = -1;

		private static float chanceLast;
	}
}
