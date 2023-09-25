using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectHatred : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataHatred buffDataHatred = new BuffDataHatred(0.6f + 0.03f * (float)this.level, 0.20000000298023224 + -0.0099999997764825821 * (double)this.level, 20f);
			buffDataHatred.id = 238;
			buffDataHatred.isPermenant = true;
			buffDataList.Add(buffDataHatred);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.6f + (float)lev * 0.03f, false));
			string text2 = base.csg(GameMath.GetPercentString(0.20000000298023224 + (double)lev * -0.0099999997764825821, false));
			string arg = base.csg(GameMath.GetTimeInMilliSecondsString(20f));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.03f, false) + ")");
				text2 += base.csg(" (" + GameMath.GetPercentString(-0.0099999997764825821, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_HATRED"), text, text2, arg);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.6f, false);
			string percentString2 = GameMath.GetPercentString(0.20000000298023224, false);
			string timeInMilliSecondsString = GameMath.GetTimeInMilliSecondsString(20f);
			return string.Format(LM.Get("TRINKET_EFFECT_HATRED"), percentString, percentString2, timeInMilliSecondsString);
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
				UiData.inst.spriteTrinketIcons[11]
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
			return "Hatred";
		}

		public const float ATTACK_SPEED_BASE = 0.6f;

		public const float ATTACK_SPEED_LEVEL = 0.03f;

		public const double DAMAGE_FACTOR_BASE = 0.20000000298023224;

		public const double DAMAGE_FACTOR_LEVEL = -0.0099999997764825821;

		public const float COOLDOWN_BASE = 20f;
	}
}
