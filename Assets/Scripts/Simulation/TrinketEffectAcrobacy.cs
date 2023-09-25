using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectAcrobacy : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataAcrobacy buffDataAcrobacy = new BuffDataAcrobacy(0.3f + 0.03f * (float)this.level, 5f + 0.5f * (float)this.level, 45f);
			buffDataAcrobacy.id = 232;
			buffDataAcrobacy.isPermenant = true;
			buffDataList.Add(buffDataAcrobacy);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.3f + (float)lev * 0.03f, false));
			string text2 = base.csg(GameMath.GetTimeInMilliSecondsString(5f + (float)lev * 0.5f));
			string arg = base.csg(GameMath.GetTimeInMilliSecondsString(45f));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.03f, false) + ")");
				text2 += base.csg(" (+" + GameMath.GetTimeInMilliSecondsString(0.5f) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_ACROBACY"), text, text2, arg);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.3f, false);
			string timeInMilliSecondsString = GameMath.GetTimeInMilliSecondsString(5f);
			string timeInMilliSecondsString2 = GameMath.GetTimeInMilliSecondsString(45f);
			return string.Format(LM.Get("TRINKET_EFFECT_ACROBACY"), percentString, timeInMilliSecondsString, timeInMilliSecondsString2);
		}

		public override float GetChanceWeight()
		{
			return 1f;
		}

		public override int GetRarity()
		{
			return 2;
		}

		public override Sprite[] GetSprites()
		{
			return new Sprite[]
			{
				UiData.inst.spriteTrinketIcons[5]
			};
		}

		public override TrinketEffectGroup GetGroup()
		{
			return TrinketEffectGroup.SPECIAL;
		}

		public override string GetDebugName()
		{
			return "Acrobacy";
		}

		public override int GetMaxLevel()
		{
			return 20;
		}

		public const float DODGE_CHANCE_BASE = 0.3f;

		public const float DODGE_CHANCE_LEVEL = 0.03f;

		public const float DURATION_BASE = 5f;

		public const float DURATION_LEVEL = 0.5f;

		public const float COOLDOWN_BASE = 45f;
	}
}
