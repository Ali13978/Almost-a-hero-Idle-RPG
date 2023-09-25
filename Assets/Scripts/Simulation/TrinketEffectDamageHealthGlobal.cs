using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectDamageHealthGlobal : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataDamageGlobalTE buffDataDamageGlobalTE = new BuffDataDamageGlobalTE();
			buffDataDamageGlobalTE.id = 263;
			buffDataDamageGlobalTE.isPermenant = true;
			buffDataDamageGlobalTE.damageAdd = 0.06 + 0.012 * (double)this.level;
			buffDataList.Add(buffDataDamageGlobalTE);
			BuffDataHealthMaxAllTE buffDataHealthMaxAllTE = new BuffDataHealthMaxAllTE();
			buffDataHealthMaxAllTE.id = 262;
			buffDataHealthMaxAllTE.isPermenant = true;
			buffDataHealthMaxAllTE.healthMaxFactorAdd = 0.06 + 0.012 * (double)this.level;
			buffDataList.Add(buffDataHealthMaxAllTE);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.06 + (double)lev * 0.012, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.012, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_DAMAGE_HEALTH_GLOBAL"), text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.06, false);
			return string.Format(LM.Get("TRINKET_EFFECT_DAMAGE_HEALTH_GLOBAL"), percentString);
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
				UiData.inst.spriteTrinketBeads[1],
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
			return "Damage Health All";
		}

		public const double DMG_BASE = 0.06;

		public const double DMG_LEVEL = 0.012;
	}
}
