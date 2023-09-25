using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectDamageTotemByHero : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataDamageTETotemByHero buffDataDamageTETotemByHero = new BuffDataDamageTETotemByHero();
			buffDataDamageTETotemByHero.id = 260;
			buffDataDamageTETotemByHero.isPermenant = true;
			buffDataDamageTETotemByHero.heroDamageRatio = 0.01 + 0.001 * (double)this.level;
			buffDataList.Add(buffDataDamageTETotemByHero);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.01 + (double)lev * 0.001, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.001, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_DAMAGE_TOTEM_BY_HERO"), text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.01, false);
			return string.Format(LM.Get("TRINKET_EFFECT_DAMAGE_TOTEM_BY_HERO"), percentString);
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
			return "Damage Totem By Hero";
		}

		public const double DMG_BASE = 0.01;

		public const double DMG_LEVEL = 0.001;
	}
}
