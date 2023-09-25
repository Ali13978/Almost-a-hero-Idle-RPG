using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectGold : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataGoldTE buffDataGoldTE = new BuffDataGoldTE();
			buffDataGoldTE.id = 225;
			buffDataGoldTE.goldAdd = 0.06 + 0.012 * (double)this.level;
			buffDataGoldTE.isPermenant = true;
			buffDataList.Add(buffDataGoldTE);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.06 + (double)lev * 0.012, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.012, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_GOLD"), text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.06, false);
			return string.Format(LM.Get("TRINKET_EFFECT_GOLD"), percentString);
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
				UiData.inst.spriteTrinketBeads[0],
				UiData.inst.spriteTrinketBeads[1]
			};
		}

		public override TrinketEffectGroup GetGroup()
		{
			return TrinketEffectGroup.COMMON;
		}

		public override int GetMaxLevel()
		{
			return 20;
		}

		public override string GetDebugName()
		{
			return "Gold";
		}

		public const double GOLD_BASE = 0.06;

		public const double GOLD_LEVEL = 0.012;
	}
}
