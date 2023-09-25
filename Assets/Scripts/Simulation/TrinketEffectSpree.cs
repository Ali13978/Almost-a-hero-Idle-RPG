using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectSpree : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataSpree buffDataSpree = new BuffDataSpree(1.0 + 0.2 * (double)this.level, 6f + -0.2f * (float)this.level);
			buffDataSpree.id = 231;
			buffDataSpree.isPermenant = true;
			buffDataList.Add(buffDataSpree);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(1.0 + (double)lev * 0.2, false));
			string text2 = base.csg(GameMath.GetTimeInMilliSecondsString(6f + -0.2f * (float)lev));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.2, false) + ")");
				text2 += base.csg(" (" + GameMath.GetTimeInMilliSecondsString(-0.2f) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_SPREE"), text, text2);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(1.0, false);
			string timeInMilliSecondsString = GameMath.GetTimeInMilliSecondsString(6f);
			return string.Format(LM.Get("TRINKET_EFFECT_SPREE"), percentString, timeInMilliSecondsString);
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
				UiData.inst.spriteTrinketIcons[4]
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
			return "Spree";
		}

		public const double DAMAGE_ADD_BASE = 1.0;

		public const double DAMAGE_ADD_LEVEL = 0.2;

		public const float COOLDOWN_BASE = 6f;

		public const float COOLDOWN_LEVEL = -0.2f;
	}
}
