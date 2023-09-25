using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectAnticipation : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataAnticipation buffDataAnticipation = new BuffDataAnticipation(2.0 + 0.20000000298023224 * (double)this.level, 10f, 150f + -10f * (float)this.level);
			buffDataAnticipation.id = 234;
			buffDataAnticipation.isPermenant = true;
			buffDataList.Add(buffDataAnticipation);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(2.0 + (double)lev * 0.20000000298023224, false));
			string arg = base.csg(GameMath.GetTimeInMilliSecondsString(10f));
			string text2 = base.csg(GameMath.GetTimeInMilliSecondsString(150f + (float)lev * -10f));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.20000000298023224, false) + ")");
				text2 += base.csg(" (" + GameMath.GetTimeInMilliSecondsString(-10f) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_ANTICIPATION"), text, arg, text2);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(2.0, false);
			string timeInMilliSecondsString = GameMath.GetTimeInMilliSecondsString(10f);
			string timeInMilliSecondsString2 = GameMath.GetTimeInMilliSecondsString(150f);
			return string.Format(LM.Get("TRINKET_EFFECT_ANTICIPATION"), percentString, timeInMilliSecondsString, timeInMilliSecondsString2);
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
				UiData.inst.spriteTrinketIcons[6]
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
			return "Anticipation";
		}

		public const double CRIT_FACTOR_ADD_BASE = 2.0;

		public const double CRIT_FACTOR_ADD_LEVEL = 0.20000000298023224;

		public const float DUR_BASE = 10f;

		public const float COOLDOWN_BASE = 150f;

		public const float COOLDOWN_LEVEL = -10f;
	}
}
