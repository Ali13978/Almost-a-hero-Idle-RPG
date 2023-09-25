using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectBlind : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataBlind buffDataBlind = new BuffDataBlind(0.05f, 3f + 0.4f * (float)this.level, 0.5f + 0.05f * (float)this.level);
			buffDataBlind.id = 252;
			buffDataBlind.isPermenant = true;
			buffDataList.Add(buffDataBlind);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string arg = base.csg(GameMath.GetPercentString(0.05f, false));
			string text = base.csg(GameMath.GetTimeInMilliSecondsString(3f + (float)lev * 0.4f));
			string text2 = base.csg(GameMath.GetPercentString(0.5f + (float)lev * 0.05f, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetTimeInMilliSecondsString(0.4f) + ")");
				text2 += base.csg(" (+" + GameMath.GetPercentString(0.05f, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_BLIND"), arg, text, text2);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.05f, false);
			string timeInMilliSecondsString = GameMath.GetTimeInMilliSecondsString(3f);
			string percentString2 = GameMath.GetPercentString(0.5f, false);
			return string.Format(LM.Get("TRINKET_EFFECT_BLIND"), percentString, timeInMilliSecondsString, percentString2);
		}

		public override float GetChanceWeight()
		{
			return 1f;
		}

		public override int GetRarity()
		{
			return 1;
		}

		public override Sprite[] GetSprites()
		{
			return new Sprite[]
			{
				UiData.inst.spriteTrinketHangers[5]
			};
		}

		public override TrinketEffectGroup GetGroup()
		{
			return TrinketEffectGroup.SECONDARY;
		}

		public override int GetMaxLevel()
		{
			return 5;
		}

		public override string GetDebugName()
		{
			return "Blind";
		}

		public const float CHANCE = 0.05f;

		public const float DUR_BASE = 3f;

		public const float DUR_LEVEL = 0.4f;

		public const float MISS_CHANCE_BASE = 0.5f;

		public const float MISS_CHANCE_LEVEL = 0.05f;
	}
}
