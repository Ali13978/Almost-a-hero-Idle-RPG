using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectSkillLifeSteal : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataSkillLifeSteal buffDataSkillLifeSteal = new BuffDataSkillLifeSteal();
			buffDataSkillLifeSteal.healRatio = (double)this.GetHealRatio(this.level);
			buffDataSkillLifeSteal.id = 229;
			buffDataSkillLifeSteal.isPermenant = true;
			buffDataList.Add(buffDataSkillLifeSteal);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.03f + (float)lev * 0.01f, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.01f, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_SKILL_LIFESTEAL"), text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.03f, false);
			return string.Format(LM.Get("TRINKET_EFFECT_SKILL_LIFESTEAL"), percentString);
		}

		private float GetHealRatio(int level)
		{
			return 0.03f + (float)level * 0.01f;
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
				UiData.inst.spriteTrinketHangers[2]
			};
		}

		public override TrinketEffectGroup GetGroup()
		{
			return TrinketEffectGroup.SECONDARY;
		}

		public override int GetMaxLevel()
		{
			return 10;
		}

		public override string GetDebugName()
		{
			return "Skill Life Steal";
		}

		public const float HEAL_RATIO_BASE = 0.03f;

		public const float HEAL_RATIO_LEVEL = 0.01f;
	}
}
