using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectBandage : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataBandage buffDataBandage = new BuffDataBandage(25f, 0.25 + (double)this.level * 0.05);
			buffDataBandage.id = 200;
			buffDataBandage.isPermenant = true;
			buffDataList.Add(buffDataBandage);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string arg = base.csg(GameMath.GetTimeInMilliSecondsString(25f));
			string text = base.csg(GameMath.GetPercentString(0.25 + (double)lev * 0.05, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.05, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_BANDAGE"), arg, text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string timeInMilliSecondsString = GameMath.GetTimeInMilliSecondsString(25f);
			string percentString = GameMath.GetPercentString(0.25, false);
			return string.Format(LM.Get("TRINKET_EFFECT_BANDAGE"), timeInMilliSecondsString, percentString);
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
				UiData.inst.spriteTrinketIcons[20]
			};
		}

		public override TrinketEffectGroup GetGroup()
		{
			return TrinketEffectGroup.SPECIAL;
		}

		public override int GetMaxLevel()
		{
			return 15;
		}

		public override string GetDebugName()
		{
			return "Bandage";
		}

		public const double HEAL_BASE = 0.25;

		public const double HEAL_LEVEL = 0.05;

		public const float PERIOD_BASE = 25f;
	}
}
