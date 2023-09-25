using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectRage : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataRage buffDataRage = new BuffDataRage(1f + 0.1f * (float)this.level, 8f + 0.5f * (float)this.level, 50f);
			buffDataRage.id = 221;
			buffDataRage.isPermenant = true;
			buffDataList.Add(buffDataRage);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(1f + (float)lev * 0.1f, false));
			string text2 = base.csg(GameMath.GetTimeInMilliSecondsString(8f + (float)lev * 0.5f));
			string arg = base.csg(GameMath.GetTimeInMilliSecondsString(50f));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.1f, false) + ")");
				text2 += base.csg(" (+" + GameMath.GetTimeInMilliSecondsString(0.5f) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_RAGE"), text, text2, arg);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(1f, false);
			string timeInMilliSecondsString = GameMath.GetTimeInMilliSecondsString(8f);
			string timeInMilliSecondsString2 = GameMath.GetTimeInMilliSecondsString(50f);
			return string.Format(LM.Get("TRINKET_EFFECT_RAGE"), percentString, timeInMilliSecondsString, timeInMilliSecondsString2);
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
				UiData.inst.spriteTrinketIcons[1]
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
			return "Windsong";
		}

		public const float ATTACK_SPEED_BASE = 1f;

		public const float ATTACK_SPEED_LEVEL = 0.1f;

		public const float RAGE_DUR_BASE = 8f;

		public const float RAGE_DUR_LEVEL = 0.5f;

		public const float EVENT_PERIOD = 50f;
	}
}
