using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectGuidance : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataTrinketGuidance buffDataTrinketGuidance = new BuffDataTrinketGuidance(0.1f + (float)this.level * 0.05f, 5f, 70f + (float)this.level * -5f);
			buffDataTrinketGuidance.id = 228;
			buffDataTrinketGuidance.isPermenant = true;
			buffDataTrinketGuidance.isStackable = false;
			buffDataList.Add(buffDataTrinketGuidance);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetTimeInMilliSecondsString(70f + (float)lev * -5f));
			string text2 = base.csg(GameMath.GetPercentString(0.1f + 0.05f * (float)lev, false));
			string arg = base.csg(GameMath.GetTimeInMilliSecondsString(5f));
			if (withUpgrade)
			{
				text += base.csg(" (" + GameMath.GetTimeInMilliSecondsString(-5f) + ")");
				text2 += base.csg(" (+" + GameMath.GetPercentString(0.05f, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_GUIDANCE"), text, text2, arg);
		}

		public override string GetDescFirstWithoutColor()
		{
			string timeInMilliSecondsString = GameMath.GetTimeInMilliSecondsString(70f);
			string percentString = GameMath.GetPercentString(0.1f, false);
			string timeInMilliSecondsString2 = GameMath.GetTimeInMilliSecondsString(5f);
			return string.Format(LM.Get("TRINKET_EFFECT_GUIDANCE"), timeInMilliSecondsString, percentString, timeInMilliSecondsString2);
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
				UiData.inst.spriteTrinketIcons[0]
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
			return "Guidance";
		}

		public const float HEAL_RATIO_BASE = 0.1f;

		public const float HEAL_RATIO_LEVEL = 0.05f;

		public const float DUR_BASE = 5f;

		public const float EVENT_PERIOD_BASE = 70f;

		public const float EVENT_PERIOD_LEVEL = -5f;
	}
}
