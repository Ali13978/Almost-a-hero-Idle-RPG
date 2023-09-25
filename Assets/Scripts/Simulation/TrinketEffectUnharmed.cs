using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectUnharmed : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataUnharmed buffDataUnharmed = new BuffDataUnharmed(1.0 + 0.4 * (double)this.level, 10f + -0.5f * (float)this.level);
			buffDataUnharmed.id = 245;
			buffDataUnharmed.isPermenant = true;
			buffDataList.Add(buffDataUnharmed);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(1.0 + (double)lev * 0.4, false));
			string text2 = base.csg(GameMath.GetTimeInMilliSecondsString(10f + -0.5f * (float)this.level));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.4, false) + ")");
				text2 += base.csg(" (" + GameMath.GetTimeInMilliSecondsString(-0.5f) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_UNHARMED"), text2, text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(1.0, false);
			string timeInMilliSecondsString = GameMath.GetTimeInMilliSecondsString(10f);
			return string.Format(LM.Get("TRINKET_EFFECT_UNHARMED"), timeInMilliSecondsString, percentString);
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
				UiData.inst.spriteTrinketIcons[18]
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
			return "Unharmed";
		}

		public const double DMG_BASE = 1.0;

		public const double DMG_LEVEL = 0.4;

		public const float PERIOD_BASE = 10f;

		public const float PERIOD_LEVEL = -0.5f;
	}
}
