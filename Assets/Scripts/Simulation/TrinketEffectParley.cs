using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectParley : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataParley buffDataParley = new BuffDataParley(0.3 + 0.05 * (double)this.level, 15f + -1f * (float)this.level);
			buffDataParley.id = 236;
			buffDataParley.isPermenant = true;
			buffDataList.Add(buffDataParley);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.3 + (double)lev * 0.05, false));
			string text2 = base.csg(GameMath.GetTimeInMilliSecondsString(15f + (float)lev * -1f));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.05, false) + ")");
				text2 += base.csg(" (" + GameMath.GetTimeInMilliSecondsString(-1f) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_PARLEY"), text, text2);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.3, false);
			string timeInMilliSecondsString = GameMath.GetTimeInMilliSecondsString(15f);
			return string.Format(LM.Get("TRINKET_EFFECT_PARLEY"), percentString, timeInMilliSecondsString);
		}

		public override float GetChanceWeight()
		{
			return 1f;
		}

		public override int GetRarity()
		{
			return 0;
		}

		public override Sprite[] GetSprites()
		{
			return new Sprite[]
			{
				UiData.inst.spriteTrinketIcons[8]
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
			return "Parley";
		}

		public const double DAMAGE_REDUCE_BASE = 0.3;

		public const double DAMAGE_REDUCE_LEVEL = 0.05;

		public const float COOLDOWN_BASE = 15f;

		public const float COOLDOWN_LEVEL = -1f;
	}
}
