using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectExtraLoot : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataTEExtraLoot buffDataTEExtraLoot = new BuffDataTEExtraLoot();
			buffDataTEExtraLoot.id = 205;
			buffDataTEExtraLoot.goldFactor = 1.5 + 0.15 * (double)this.level;
			buffDataTEExtraLoot.isPermenant = true;
			buffDataList.Add(buffDataTEExtraLoot);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.5 + (double)lev * 0.15, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.15, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_EXTRA_LOOT"), text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.5, false);
			return string.Format(LM.Get("TRINKET_EFFECT_EXTRA_LOOT"), percentString);
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
			return "Extra Loot";
		}

		public const double GOLD_BASE = 0.5;

		public const double GOLD_LEVEL = 0.15;
	}
}
