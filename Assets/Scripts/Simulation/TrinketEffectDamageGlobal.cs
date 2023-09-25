using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectDamageGlobal : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataDamageGlobalTE buffDataDamageGlobalTE = new BuffDataDamageGlobalTE();
			buffDataDamageGlobalTE.id = 203;
			buffDataDamageGlobalTE.isPermenant = true;
			buffDataDamageGlobalTE.damageAdd = 0.1 + 0.02 * (double)this.level;
			buffDataList.Add(buffDataDamageGlobalTE);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.1 + (double)lev * 0.02, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.02, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_DAMAGE_GLOBAL"), text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.1, false);
			return string.Format(LM.Get("TRINKET_EFFECT_DAMAGE_GLOBAL"), percentString);
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
			return "Damage All";
		}

		public const double DMG_BASE = 0.1;

		public const double DMG_LEVEL = 0.02;
	}
}
