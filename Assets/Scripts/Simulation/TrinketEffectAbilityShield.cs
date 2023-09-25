using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectAbilityShield : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataAbilityShield buffDataAbilityShield = new BuffDataAbilityShield(1f - (0.05f + (float)this.level * 0.05f), 10f);
			buffDataAbilityShield.isPermenant = true;
			buffDataAbilityShield.id = 291;
			buffDataList.Add(buffDataAbilityShield);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.05f + (float)lev * 0.05f, false));
			string arg = base.csg(GameMath.GetTimeInMilliSecondsString(10f));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.05f, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_ABILITYSHIELD"), text, arg);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.05f, false);
			string timeInMilliSecondsString = GameMath.GetTimeInMilliSecondsString(10f);
			return string.Format(LM.Get("TRINKET_EFFECT_ABILITYSHIELD"), percentString, timeInMilliSecondsString);
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
				UiData.inst.spriteTrinketHangers[12]
			};
		}

		public override TrinketEffectGroup GetGroup()
		{
			return TrinketEffectGroup.SECONDARY;
		}

		public override string GetDebugName()
		{
			return "AbilityShield";
		}

		public override int GetMaxLevel()
		{
			return 15;
		}

		public const float DAMAGE_REDUCTION_BASE = 0.05f;

		public const float DAMAGE_REDUCTION_LEVEL = 0.05f;

		public const float DAMAGE_REDUCTION_TIME = 10f;
	}
}
