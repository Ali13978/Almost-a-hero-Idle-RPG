using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectHarmfulShield : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataHarmfulShield buffDataHarmfulShield = new BuffDataHarmfulShield(0.25 + 0.05000000074505806 * (double)this.level);
			buffDataHarmfulShield.id = 248;
			buffDataHarmfulShield.isPermenant = true;
			buffDataList.Add(buffDataHarmfulShield);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.25 + (double)lev * 0.05000000074505806, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.05000000074505806, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_HARMFUL_SHIELD"), text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.25, false);
			return string.Format(LM.Get("TRINKET_EFFECT_HARMFUL_SHIELD"), percentString);
		}

		public override float GetChanceWeight()
		{
			return 1f;
		}

		public override int GetRarity()
		{
			return 1;
		}

		public override Sprite[] GetSprites()
		{
			return new Sprite[]
			{
				UiData.inst.spriteTrinketHangers[0]
			};
		}

		public override TrinketEffectGroup GetGroup()
		{
			return TrinketEffectGroup.SECONDARY;
		}

		public override string GetDebugName()
		{
			return "Harmful Shield";
		}

		public override int GetMaxLevel()
		{
			return 15;
		}

		public const double DAMAGE_BASE = 0.25;

		public const double DAMAGE_LEVEL = 0.05000000074505806;
	}
}
