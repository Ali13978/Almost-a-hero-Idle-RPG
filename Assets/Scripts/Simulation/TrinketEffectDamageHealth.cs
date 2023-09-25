using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectDamageHealth : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataDamageTE buffDataDamageTE = new BuffDataDamageTE();
			buffDataDamageTE.id = 266;
			buffDataDamageTE.isPermenant = true;
			buffDataDamageTE.damageAdd = 0.2 + 0.04 * (double)this.level;
			buffDataList.Add(buffDataDamageTE);
			BuffDataHealthMaxTE buffDataHealthMaxTE = new BuffDataHealthMaxTE();
			buffDataHealthMaxTE.id = 267;
			buffDataHealthMaxTE.isPermenant = true;
			buffDataHealthMaxTE.healthMaxFactorAdd = 0.2 + 0.04 * (double)this.level;
			buffDataList.Add(buffDataHealthMaxTE);
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
			return string.Format(LM.Get("TRINKET_EFFECT_DAMAGE_HEALTH"), text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.2, false);
			return string.Format(LM.Get("TRINKET_EFFECT_DAMAGE_HEALTH"), percentString);
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
			return "Damage Health";
		}

		public const double DMG_AND_HEALTH_BASE = 0.2;

		public const double DMG_AND_HEALTH_LEVEL = 0.04;
	}
}
