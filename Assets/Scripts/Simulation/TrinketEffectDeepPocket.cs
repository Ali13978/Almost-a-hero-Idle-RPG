using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectDeepPocket : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataDeepPocket buffDataDeepPocket = new BuffDataDeepPocket(0.25 + 0.05000000074505806 * (double)this.level, 10, 30f);
			buffDataDeepPocket.id = 242;
			buffDataDeepPocket.isPermenant = true;
			buffDataList.Add(buffDataDeepPocket);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.25 + (double)lev * 0.05000000074505806, false));
			string arg = base.csg(GameMath.GetTimeInMilliSecondsString(10f));
			string arg2 = base.csg(GameMath.GetTimeInMilliSecondsString(30f));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.05000000074505806, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_DEEP_POCKET"), text, arg, arg2);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.25, false);
			string timeInMilliSecondsString = GameMath.GetTimeInMilliSecondsString(10f);
			string timeInMilliSecondsString2 = GameMath.GetTimeInMilliSecondsString(30f);
			return string.Format(LM.Get("TRINKET_EFFECT_DEEP_POCKET"), percentString, timeInMilliSecondsString, timeInMilliSecondsString2);
		}

		public override float GetChanceWeight()
		{
			return 1f;
		}

		public override int GetRarity()
		{
			return 5;
		}

		public override Sprite[] GetSprites()
		{
			return new Sprite[]
			{
				UiData.inst.spriteTrinketIcons[14]
			};
		}

		public override TrinketEffectGroup GetGroup()
		{
			return TrinketEffectGroup.SPECIAL;
		}

		public override int GetMaxLevel()
		{
			return 20;
		}

		public override string GetDebugName()
		{
			return "Deep Pocket";
		}

		public const double GOLD_BASE = 0.25;

		public const double GOLD_LEVEL = 0.05000000074505806;

		public const int NUM_DROP_TIMES_BASE = 10;

		public const float COOLDOWN_BASE = 30f;
	}
}
