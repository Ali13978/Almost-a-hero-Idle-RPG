using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectFury : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataFury buffDataFury = new BuffDataFury(1f + 0.1f * (float)this.level, 5f + 0.5f * (float)this.level, 60f);
			buffDataFury.id = 229;
			buffDataFury.isPermenant = true;
			buffDataList.Add(buffDataFury);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetTimeInMilliSecondsString(1f + (float)lev * 0.1f));
			string text2 = base.csg(GameMath.GetTimeInMilliSecondsString(5f + 0.5f * (float)lev));
			string arg = base.csg(GameMath.GetTimeInMilliSecondsString(60f));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetTimeInMilliSecondsString(0.1f) + ")");
				text2 += base.csg(" (+" + GameMath.GetTimeInMilliSecondsString(0.5f) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_FURY"), text, text2, arg);
		}

		public override string GetDescFirstWithoutColor()
		{
			string timeInMilliSecondsString = GameMath.GetTimeInMilliSecondsString(1f);
			string timeInMilliSecondsString2 = GameMath.GetTimeInMilliSecondsString(5f);
			string timeInMilliSecondsString3 = GameMath.GetTimeInMilliSecondsString(60f);
			return string.Format(LM.Get("TRINKET_EFFECT_FURY"), timeInMilliSecondsString, timeInMilliSecondsString2, timeInMilliSecondsString3);
		}

		public override float GetChanceWeight()
		{
			return 1f;
		}

		public override int GetRarity()
		{
			return 3;
		}

		public override Sprite[] GetSprites()
		{
			return new Sprite[]
			{
				UiData.inst.spriteTrinketIcons[3]
			};
		}

		public override TrinketEffectGroup GetGroup()
		{
			return TrinketEffectGroup.SPECIAL;
		}

		public override int GetMaxLevel()
		{
			return 10;
		}

		public override string GetDebugName()
		{
			return "Fury";
		}

		public const float DECREASE_BASE = 1f;

		public const float DECREASE_LEVEL = 0.1f;

		public const float DUR_BASE = 5f;

		public const float DUR_LEVEL = 0.5f;

		public const float COOLDOWN_BASE = 60f;
	}
}
