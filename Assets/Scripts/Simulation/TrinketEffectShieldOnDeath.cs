using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectShieldOnDeath : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			ShieldOnDeath shieldOnDeath = new ShieldOnDeath(90f + -6f * (float)this.level, 0.1f + 0.05f * (float)this.level);
			shieldOnDeath.id = 294;
			shieldOnDeath.isPermenant = true;
			buffDataList.Add(shieldOnDeath);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.1f + 0.05f * (float)this.level, false));
			string text2 = base.csg(GameMath.GetTimeInSecondsString(90f + -6f * (float)this.level));
			if (withUpgrade)
			{
				text += base.csg(" (" + GameMath.GetPercentString(0.05f, false) + ")");
				text2 += base.csg(" (" + GameMath.GetTimeInSecondsString(-6f) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_SHIELD_ON_DEATH"), text, text2);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.1f, false);
			string timeInSecondsString = GameMath.GetTimeInSecondsString(90f);
			return string.Format(LM.Get("TRINKET_EFFECT_SHIELD_ON_DEATH"), percentString, timeInSecondsString);
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
				UiData.inst.spriteTrinketIcons[24]
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
			return "ShieldOnDeath";
		}

		public const float SHIELD_AMOUNT_BASE = 0.1f;

		public const float SHIELD_AMOUNT_LEVEL = 0.05f;

		public const float PERIOD_CD_BASE = 90f;

		public const float PERIOD_CS_LEVEL = -6f;
	}
}
