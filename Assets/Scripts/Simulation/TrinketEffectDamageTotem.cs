using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectDamageTotem : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataDamageTETotem buffDataDamageTETotem = new BuffDataDamageTETotem();
			buffDataDamageTETotem.id = 204;
			buffDataDamageTETotem.isPermenant = true;
			buffDataDamageTETotem.damageAdd = 0.2 + 0.04 * (double)this.level;
			buffDataList.Add(buffDataDamageTETotem);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.2 + (double)lev * 0.04, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.04, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_DAMAGE_TOTEM"), text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.2, false);
			return string.Format(LM.Get("TRINKET_EFFECT_DAMAGE_TOTEM"), percentString);
		}

		public override float GetChanceWeight()
		{
			return 1f;
		}

		public override int GetRarity()
		{
			return 4;
		}

		public override Sprite[] GetSprites()
		{
			return new Sprite[]
			{
				UiData.inst.spriteTrinketBeads[1],
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
			return 20;
		}

		public override string GetDebugName()
		{
			return "Damage Totem";
		}

		public const double DMG_BASE = 0.2;

		public const double DMG_LEVEL = 0.04;
	}
}
