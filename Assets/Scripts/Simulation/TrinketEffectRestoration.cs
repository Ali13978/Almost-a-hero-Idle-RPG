using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectRestoration : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataRestoration buffDataRestoration = new BuffDataRestoration(0.30000001192092896 + 0.0099999997764825821 * (double)this.level, 90f + -4f * (float)this.level);
			buffDataRestoration.id = 238;
			buffDataRestoration.isPermenant = true;
			buffDataList.Add(buffDataRestoration);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.30000001192092896 + (double)lev * 0.0099999997764825821, false));
			string arg = base.csg(GameMath.GetTimeInMilliSecondsString(3f));
			string text2 = base.csg(GameMath.GetTimeInMilliSecondsString(90f + (float)lev * -4f));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.0099999997764825821, false) + ")");
				text2 += base.csg(" (" + GameMath.GetTimeInMilliSecondsString(-4f) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_RESTORATION"), text, arg, text2);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.30000001192092896, false);
			string timeInMilliSecondsString = GameMath.GetTimeInMilliSecondsString(3f);
			string timeInMilliSecondsString2 = GameMath.GetTimeInMilliSecondsString(90f);
			return string.Format(LM.Get("TRINKET_EFFECT_RESTORATION"), percentString, timeInMilliSecondsString, timeInMilliSecondsString2);
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
				UiData.inst.spriteTrinketIcons[10]
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
			return "Restoration";
		}

		public const double HEALTH_REGEN_BASE = 0.30000001192092896;

		public const double HEALTH_REGEN_LEVEL = 0.0099999997764825821;

		public const float DUR_BASE = 3f;

		public const float COOLDOWN_BASE = 90f;

		public const float COOLDOWN_LEVEL = -4f;
	}
}
