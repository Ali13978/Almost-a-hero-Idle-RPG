using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectCurse : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataCurse buffDataCurse = new BuffDataCurse(0.05f, 8f, 0.20000000298023224 + 0.019999999552965164 * (double)this.level);
			buffDataCurse.id = 253;
			buffDataCurse.isPermenant = true;
			buffDataList.Add(buffDataCurse);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string arg = base.csg(GameMath.GetPercentString(0.05f, false));
			string arg2 = base.csg(GameMath.GetTimeInMilliSecondsString(8f));
			string text = base.csg(GameMath.GetPercentString(0.20000000298023224 + (double)lev * 0.019999999552965164, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.019999999552965164, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_CURSE"), arg, arg2, text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.05f, false);
			string timeInMilliSecondsString = GameMath.GetTimeInMilliSecondsString(8f);
			string percentString2 = GameMath.GetPercentString(0.20000000298023224, false);
			return string.Format(LM.Get("TRINKET_EFFECT_CURSE"), percentString, timeInMilliSecondsString, percentString2);
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
				UiData.inst.spriteTrinketHangers[6]
			};
		}

		public override TrinketEffectGroup GetGroup()
		{
			return TrinketEffectGroup.SECONDARY;
		}

		public override int GetMaxLevel()
		{
			return 10;
		}

		public override string GetDebugName()
		{
			return "Curse";
		}

		public const float CHANCE = 0.05f;

		public const float DUR_BASE = 8f;

		public const double DAMAGE_TAKE_FACTOR_BASE = 0.20000000298023224;

		public const double DAMAGE_TAKE_FACTOR_LEVEL = 0.019999999552965164;
	}
}
