using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectReviveHalf : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataReviveHalf buffDataReviveHalf = new BuffDataReviveHalf();
			buffDataReviveHalf.id = 219;
			buffDataReviveHalf.isPermenant = true;
			buffDataReviveHalf.chance = 0.2f + 0.06f * (float)this.level;
			buffDataList.Add(buffDataReviveHalf);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.2f + (float)lev * 0.06f, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.06f, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_REVIVE_HALF"), text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.2f, false);
			return string.Format(LM.Get("TRINKET_EFFECT_REVIVE_HALF"), percentString);
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
				UiData.inst.spriteTrinketIcons[22]
			};
		}

		public override TrinketEffectGroup GetGroup()
		{
			return TrinketEffectGroup.SPECIAL;
		}

		public override int GetMaxLevel()
		{
			return 10;
		}

		public override string GetDebugName()
		{
			return "Revive Half";
		}

		public const float CHANCE_BASE = 0.2f;

		public const float CHANCE_LEVEL = 0.06f;
	}
}
