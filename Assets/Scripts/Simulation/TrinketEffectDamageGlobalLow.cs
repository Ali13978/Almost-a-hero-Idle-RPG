using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectDamageGlobalLow : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataDamageGlobalTE buffDataDamageGlobalTE = new BuffDataDamageGlobalTE();
			buffDataDamageGlobalTE.id = 203;
			buffDataDamageGlobalTE.isPermenant = true;
			buffDataDamageGlobalTE.damageAdd = 0.05 + 0.015 * (double)this.level;
			buffDataList.Add(buffDataDamageGlobalTE);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.05 + (double)lev * 0.015, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.015, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_DAMAGE_GLOBAL"), text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.05, false);
			return string.Format(LM.Get("TRINKET_EFFECT_DAMAGE_GLOBAL"), percentString);
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
				UiData.inst.spriteTrinketBeads[0],
				UiData.inst.spriteTrinketBeads[0],
				UiData.inst.spriteTrinketBeads[2]
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
			return "Damage All Low";
		}

		public const double DMG_BASE = 0.05;

		public const double DMG_LEVEL = 0.015;
	}
}
