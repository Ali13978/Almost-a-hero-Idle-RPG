using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectLightning : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataLightning buffDataLightning = new BuffDataLightning(1.0 + 0.2 * (double)this.level, 5f);
			buffDataLightning.id = 207;
			buffDataLightning.isPermenant = true;
			buffDataList.Add(buffDataLightning);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(1.0 + (double)lev * 0.2, false));
			string arg = base.csg(GameMath.GetTimeInMilliSecondsString(5f));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.2, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_LIGHTNING"), text, arg);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(1.0, false);
			string timeInMilliSecondsString = GameMath.GetTimeInMilliSecondsString(5f);
			return string.Format(LM.Get("TRINKET_EFFECT_LIGHTNING"), percentString, timeInMilliSecondsString);
		}

		public override float GetChanceWeight()
		{
			return 1f;
		}

		public override int GetRarity()
		{
			return 4;
		}

		public override Sprite[] GetSprites()
		{
			return new Sprite[]
			{
				UiData.inst.spriteTrinketIcons[16]
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
			return "Strike";
		}

		public const double DMG_BASE = 1.0;

		public const double DMG_LEVEL = 0.2;

		public const float PERIOD = 5f;
	}
}
