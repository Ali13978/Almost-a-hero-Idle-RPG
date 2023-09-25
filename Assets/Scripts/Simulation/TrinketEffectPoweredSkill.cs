using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectPoweredSkill : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataPoweredSkill buffDataPoweredSkill = new BuffDataPoweredSkill((double)(1.2f + 0.04f * (float)this.level));
			buffDataPoweredSkill.id = 256;
			buffDataPoweredSkill.isPermenant = true;
			buffDataList.Add(buffDataPoweredSkill);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.2f + (float)lev * 0.04f, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.04f, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_POWERED_SKILL"), text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.2f, false);
			return string.Format(LM.Get("TRINKET_EFFECT_POWERED_SKILL"), percentString);
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
				UiData.inst.spriteTrinketHangers[8]
			};
		}

		public override TrinketEffectGroup GetGroup()
		{
			return TrinketEffectGroup.SECONDARY;
		}

		public override string GetDebugName()
		{
			return "Powered Skill";
		}

		public override int GetMaxLevel()
		{
			return 20;
		}

		public const float SKILL_POWER_FACTOR_BASE = 0.2f;

		public const float SKILL_POWER_FACTOR_LEVEL = 0.04f;
	}
}
