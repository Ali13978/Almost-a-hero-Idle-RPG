using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectFountain : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataFountain buffDataFountain = new BuffDataFountain(1f + (0.1f + (float)this.level * 0.02f));
			buffDataFountain.isPermenant = true;
			buffDataFountain.id = 102;
			buffDataList.Add(buffDataFountain);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.1f + (float)lev * 0.02f, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.02f, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_FOUNTAIN"), text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.1f, false);
			return string.Format(LM.Get("TRINKET_EFFECT_FOUNTAIN"), percentString);
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
				UiData.inst.spriteTrinketBeads[2],
				UiData.inst.spriteTrinketBeads[0],
				UiData.inst.spriteTrinketBeads[0]
			};
		}

		public override TrinketEffectGroup GetGroup()
		{
			return TrinketEffectGroup.COMMON;
		}

		public override string GetDebugName()
		{
			return "Fountain";
		}

		public override int GetMaxLevel()
		{
			return 20;
		}

		public const float REGEN_INCREASE_BASE = 0.1f;

		public const float REGEN_INCREASE_LEVEL = 0.02f;
	}
}
