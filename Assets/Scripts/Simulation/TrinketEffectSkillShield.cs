using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectSkillShield : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataSkillShield buffDataSkillShield = new BuffDataSkillShield(0.2f + 0.04f * (float)this.level);
			buffDataSkillShield.id = 250;
			buffDataSkillShield.isPermenant = true;
			buffDataList.Add(buffDataSkillShield);
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
			return string.Format(LM.Get("TRINKET_EFFECT_SKILL_SHIELD"), text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.2f, false);
			return string.Format(LM.Get("TRINKET_EFFECT_SKILL_SHIELD"), percentString);
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
				UiData.inst.spriteTrinketHangers[2]
			};
		}

		public override TrinketEffectGroup GetGroup()
		{
			return TrinketEffectGroup.SECONDARY;
		}

		public override string GetDebugName()
		{
			return "Skill Shield";
		}

		public override int GetMaxLevel()
		{
			return 10;
		}

		public const float SKILL_CD_BASE = 0.2f;

		public const float SKILL_CD_LEVEL = 0.04f;
	}
}
