using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectHemorrhage : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataHemorrhage buffDataHemorrhage = new BuffDataHemorrhage(1.0 + 0.10000000149011612 * (double)this.level, 15f + -0.5f * (float)this.level, 3f);
			buffDataHemorrhage.id = 240;
			buffDataHemorrhage.isPermenant = true;
			buffDataList.Add(buffDataHemorrhage);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(1.0 + (double)lev * 0.10000000149011612, false));
			string arg = base.csg(GameMath.GetTimeInMilliSecondsString(3f));
			string text2 = base.csg(GameMath.GetTimeInMilliSecondsString(15f + (float)lev * -0.5f));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.10000000149011612, false) + ")");
				text2 += base.csg(" (" + GameMath.GetTimeInMilliSecondsString(-0.5f) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_HEMORRHAGE"), text, arg, text2);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(1.0, false);
			string timeInMilliSecondsString = GameMath.GetTimeInMilliSecondsString(3f);
			string timeInMilliSecondsString2 = GameMath.GetTimeInMilliSecondsString(15f);
			return string.Format(LM.Get("TRINKET_EFFECT_HEMORRHAGE"), percentString, timeInMilliSecondsString, timeInMilliSecondsString2);
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
				UiData.inst.spriteTrinketIcons[12]
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
			return "Hemorrhage";
		}

		public const double DAMAGE_BASE = 1.0;

		public const double DAMAGE_LEVEL = 0.10000000149011612;

		public const float DUR_BASE = 3f;

		public const float COOLDOWN_BASE = 15f;

		public const float COOLDOWN_LEVEL = -0.5f;
	}
}
