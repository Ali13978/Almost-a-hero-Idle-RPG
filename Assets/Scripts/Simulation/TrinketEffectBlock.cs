using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectBlock : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataBlock buffDataBlock = new BuffDataBlock();
			buffDataBlock.id = 259;
			buffDataBlock.chance = 0.3f + 0.03f * (float)this.level;
			buffDataBlock.damageBlockFactor = 0.3 + 0.03 * (double)this.level;
			buffDataBlock.isPermenant = true;
			buffDataList.Add(buffDataBlock);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.3f + (float)lev * 0.03f, false));
			string text2 = base.csg(GameMath.GetPercentString(0.3 + (double)lev * 0.03, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.03f, false) + ")");
				text2 += base.csg(" (+" + GameMath.GetPercentString(0.03, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_BLOCK"), text, text2);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.3f, false);
			string percentString2 = GameMath.GetPercentString(0.3, false);
			return string.Format(LM.Get("TRINKET_EFFECT_BLOCK"), percentString, percentString2);
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
				UiData.inst.spriteTrinketHangers[11]
			};
		}

		public override TrinketEffectGroup GetGroup()
		{
			return TrinketEffectGroup.SECONDARY;
		}

		public override int GetMaxLevel()
		{
			return 10;
		}

		public override string GetDebugName()
		{
			return "Block";
		}

		public const float CHANCE_BASE = 0.3f;

		public const float CHANCE_LEVEL = 0.03f;

		public const double BLOCK_DMG_BASE = 0.3;

		public const double BLOCK_DMG_LEVEL = 0.03;
	}
}
