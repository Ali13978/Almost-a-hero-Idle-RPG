using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectUpgradeCostAll : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataUpgradeCostReduceAllTE buffDataUpgradeCostReduceAllTE = new BuffDataUpgradeCostReduceAllTE();
			buffDataUpgradeCostReduceAllTE.id = 210;
			buffDataUpgradeCostReduceAllTE.isPermenant = true;
			buffDataUpgradeCostReduceAllTE.reductionRatio = 0.02 + 0.003 * (double)this.level;
			buffDataList.Add(buffDataUpgradeCostReduceAllTE);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.02 + (double)lev * 0.003, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.003, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_UPGRADE_COST_ALL"), text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.02, false);
			return string.Format(LM.Get("TRINKET_EFFECT_UPGRADE_COST_ALL"), percentString);
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
				UiData.inst.spriteTrinketBeads[1],
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
			return 10;
		}

		public override string GetDebugName()
		{
			return "Upgrade Cost All";
		}

		public const double REDUCTION_BASE = 0.02;

		public const double REDUCTION_LEVEL = 0.003;
	}
}
