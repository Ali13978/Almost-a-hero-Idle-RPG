using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectLightningShield : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataLightningShield buffDataLightningShield = new BuffDataLightningShield(1.0 + 0.2 * (double)this.level, 10f, 80f + -2f * (float)this.level);
			buffDataLightningShield.id = 227;
			buffDataLightningShield.isPermenant = true;
			buffDataList.Add(buffDataLightningShield);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(1.0 + (double)lev * 0.2, false));
			string arg = base.csg(GameMath.GetTimeInMilliSecondsString(10f));
			string text2 = base.csg(GameMath.GetTimeInMilliSecondsString(80f + (float)lev * -2f));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.2, false) + ")");
				text2 += base.csg(" (" + GameMath.GetTimeInMilliSecondsString(-2f) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_LIGHTNING_SHIELD"), text, arg, text2);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(1.0, false);
			string timeInMilliSecondsString = GameMath.GetTimeInMilliSecondsString(10f);
			string timeInMilliSecondsString2 = GameMath.GetTimeInMilliSecondsString(80f);
			return string.Format(LM.Get("TRINKET_EFFECT_LIGHTNING_SHIELD"), percentString, timeInMilliSecondsString, timeInMilliSecondsString2);
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
				UiData.inst.spriteTrinketIcons[2]
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
			return "Lightning Shield";
		}

		public const double DMG_BASE = 1.0;

		public const double DMG_LEVEL = 0.2;

		public const float DEBUFF_DUR = 10f;

		public const float COOLDOWN_BASE = 80f;

		public const float COOLDOWN_LEVEL = -2f;
	}
}
