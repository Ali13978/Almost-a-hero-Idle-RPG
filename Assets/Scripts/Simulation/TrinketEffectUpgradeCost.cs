using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectUpgradeCost : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataUpgradeCostReduceTE buffDataUpgradeCostReduceTE = new BuffDataUpgradeCostReduceTE();
			buffDataUpgradeCostReduceTE.id = 210;
			buffDataUpgradeCostReduceTE.isPermenant = true;
			buffDataUpgradeCostReduceTE.reductionRatio = 0.05 + 0.01 * (double)this.level;
			buffDataList.Add(buffDataUpgradeCostReduceTE);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.05 + (double)lev * 0.01, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.01, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_UPGRADE_COST"), text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.05, false);
			return string.Format(LM.Get("TRINKET_EFFECT_UPGRADE_COST"), percentString);
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
				UiData.inst.spriteTrinketBeads[1],
				UiData.inst.spriteTrinketBeads[0]
			};
		}

		public override TrinketEffectGroup GetGroup()
		{
			return TrinketEffectGroup.COMMON;
		}

		public override int GetMaxLevel()
		{
			return 10;
		}

		public override string GetDebugName()
		{
			return "Upgrade Cost";
		}

		public const double REDUCTION_BASE = 0.05;

		public const double REDUCTION_LEVEL = 0.01;
	}
}
