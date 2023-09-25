using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectAlacrity : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataAlacrity buffDataAlacrity = new BuffDataAlacrity(2f + 0.4f * (float)this.level, 20f + -0.5f * (float)this.level);
			buffDataAlacrity.id = 237;
			buffDataAlacrity.isPermenant = true;
			buffDataList.Add(buffDataAlacrity);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetTimeInMilliSecondsString(2f + (float)lev * 0.4f));
			string text2 = base.csg(GameMath.GetTimeInMilliSecondsString(20f + (float)lev * -0.5f));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetTimeInMilliSecondsString(0.4f) + ")");
				text2 += base.csg(" (" + GameMath.GetTimeInMilliSecondsString(-0.5f) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_ALACRITY"), text, text2);
		}

		public override string GetDescFirstWithoutColor()
		{
			string timeInMilliSecondsString = GameMath.GetTimeInMilliSecondsString(2f);
			string timeInMilliSecondsString2 = GameMath.GetTimeInMilliSecondsString(20f);
			return string.Format(LM.Get("TRINKET_EFFECT_ALACRITY"), timeInMilliSecondsString, timeInMilliSecondsString2);
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
				UiData.inst.spriteTrinketIcons[9]
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
			return "Alacrity";
		}

		public const float CD_REDUCE_BASE = 2f;

		public const float CD_REDUCE_LEVEL = 0.4f;

		public const float COOLDOWN_BASE = 20f;

		public const float COOLDOWN_LEVEL = -0.5f;
	}
}
