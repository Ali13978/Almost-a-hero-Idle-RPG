using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectSkillReduceUpgradeCost : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataSkillReduceUpgradeCost buffDataSkillReduceUpgradeCost = new BuffDataSkillReduceUpgradeCost();
			buffDataSkillReduceUpgradeCost.id = 224;
			buffDataSkillReduceUpgradeCost.isPermenant = true;
			buffDataSkillReduceUpgradeCost.reductionOnSkill = 0.03 + 0.004 * (double)this.level;
			buffDataSkillReduceUpgradeCost.reductionMax = 0.5;
			buffDataList.Add(buffDataSkillReduceUpgradeCost);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.03 + (double)lev * 0.004, false));
			string arg = base.csg(GameMath.GetPercentString(0.5, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.004, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_SKILL_REDUCE_UPGRADE_COST"), text, arg);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.03, false);
			string percentString2 = GameMath.GetPercentString(0.5, false);
			return string.Format(LM.Get("TRINKET_EFFECT_SKILL_REDUCE_UPGRADE_COST"), percentString, percentString2);
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
				UiData.inst.spriteTrinketBeads[0],
				UiData.inst.spriteTrinketBeads[0],
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
			return "Skill Up. Cost";
		}

		public const double REDUCTION_BASE = 0.03;

		public const double REDUCTION_LEVEL = 0.004;

		public const double REDUCTION_MAX = 0.5;
	}
}
