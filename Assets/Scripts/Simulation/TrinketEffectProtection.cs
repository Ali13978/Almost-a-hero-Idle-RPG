using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectProtection : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataProtection buffDataProtection = new BuffDataProtection(0.25 + 0.05000000074505806 * (double)this.level, 30f);
			buffDataProtection.id = 243;
			buffDataProtection.isPermenant = true;
			buffDataList.Add(buffDataProtection);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.25 + (double)lev * 0.05000000074505806, false));
			string arg = base.csg(GameMath.GetTimeInMilliSecondsString(30f));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.05000000074505806, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_PROTECTION"), text, arg);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.25, false);
			string timeInMilliSecondsString = GameMath.GetTimeInMilliSecondsString(30f);
			return string.Format(LM.Get("TRINKET_EFFECT_PROTECTION"), percentString, timeInMilliSecondsString);
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
				UiData.inst.spriteTrinketIcons[15]
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
			return "Protection";
		}

		public const double SHIELD_BASE = 0.25;

		public const double SHIELD_LEVEL = 0.05000000074505806;

		public const float COOLDOWN_BASE = 30f;
	}
}
