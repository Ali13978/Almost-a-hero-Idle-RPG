using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectDamage : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataDamageTE buffDataDamageTE = new BuffDataDamageTE();
			buffDataDamageTE.id = 201;
			buffDataDamageTE.isPermenant = true;
			buffDataDamageTE.damageAdd = 0.3 + 0.06 * (double)this.level;
			buffDataList.Add(buffDataDamageTE);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.3 + (double)lev * 0.06, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.06, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_DAMAGE"), text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.3, false);
			return string.Format(LM.Get("TRINKET_EFFECT_DAMAGE"), percentString);
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
				UiData.inst.spriteTrinketBeads[1],
				UiData.inst.spriteTrinketBeads[1],
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
			return "Damage";
		}

		public const double DMG_BASE = 0.3;

		public const double DMG_LEVEL = 0.06;
	}
}
