using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectCheatDeath : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataCheatDeath buffDataCheatDeath = new BuffDataCheatDeath(0.2 + 0.04 * (double)this.level, 90f + -2f * (float)this.level);
			buffDataCheatDeath.id = 235;
			buffDataCheatDeath.isPermenant = true;
			buffDataList.Add(buffDataCheatDeath);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.2 + (double)lev * 0.04, false));
			string text2 = base.csg(GameMath.GetTimeInMilliSecondsString(90f + (float)lev * -2f));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.04, false) + ")");
				text2 += base.csg(" (" + GameMath.GetTimeInMilliSecondsString(-2f) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_CHEAT_DEATH"), text, text2);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.2, false);
			string timeInMilliSecondsString = GameMath.GetTimeInMilliSecondsString(90f);
			return string.Format(LM.Get("TRINKET_EFFECT_CHEAT_DEATH"), percentString, timeInMilliSecondsString);
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
				UiData.inst.spriteTrinketIcons[7]
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
			return "Cheat Death";
		}

		public const double HEALTH_BASE = 0.2;

		public const double HEALTH_LEVEL = 0.04;

		public const float COOLDOWN_BASE = 90f;

		public const float COOLDOWN_LEVEL = -2f;
	}
}
