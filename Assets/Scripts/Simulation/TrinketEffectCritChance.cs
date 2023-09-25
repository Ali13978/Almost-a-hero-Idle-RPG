using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectCritChance : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataCritChance buffDataCritChance = new BuffDataCritChance();
			buffDataCritChance.id = 268;
			buffDataCritChance.isPermenant = true;
			buffDataCritChance.critChanceAdd = 0.1f + 0.025f * (float)this.level;
			buffDataList.Add(buffDataCritChance);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.1f + (float)lev * 0.025f, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.025f, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_CRIT_CHANCE"), text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.1f, false);
			return string.Format(LM.Get("TRINKET_EFFECT_CRIT_CHANCE"), percentString);
		}

		public override float GetChanceWeight()
		{
			return 1f;
		}

		public override int GetRarity()
		{
			return 0;
		}

		public override Sprite[] GetSprites()
		{
			return new Sprite[]
			{
				UiData.inst.spriteTrinketBeads[1],
				UiData.inst.spriteTrinketBeads[1]
			};
		}

		public override TrinketEffectGroup GetGroup()
		{
			return TrinketEffectGroup.COMMON;
		}

		public override string GetDebugName()
		{
			return "Crit Chance";
		}

		public override int GetMaxLevel()
		{
			return 10;
		}

		public const float CRIT_CHANCE_BASE = 0.1f;

		public const float CRIT_CHANCE_LEVEL = 0.025f;
	}
}
