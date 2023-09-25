using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectGoldyStand : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataGoldyStand buffDataGoldyStand = new BuffDataGoldyStand(10, 1f + 0.2f * (float)this.level);
			buffDataGoldyStand.id = 292;
			buffDataGoldyStand.isPermenant = true;
			buffDataList.Add(buffDataGoldyStand);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string arg = base.csg(10.ToString());
			string text = base.csg(GameMath.GetPercentString(1f + 0.2f * (float)this.level, false));
			if (withUpgrade)
			{
				text += base.csg(" (" + GameMath.GetPercentString(0.2f, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_GOLDYSTAND"), arg, text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string arg = 10.ToString();
			string percentString = GameMath.GetPercentString(1f, false);
			return string.Format(LM.Get("TRINKET_EFFECT_GOLDYSTAND"), arg, percentString);
		}

		public override float GetChanceWeight()
		{
			return 1f;
		}

		public override int GetRarity()
		{
			return 5;
		}

		public override Sprite[] GetSprites()
		{
			return new Sprite[]
			{
				UiData.inst.spriteTrinketHangers[13]
			};
		}

		public override TrinketEffectGroup GetGroup()
		{
			return TrinketEffectGroup.SECONDARY;
		}

		public override int GetMaxLevel()
		{
			return 15;
		}

		public override string GetDebugName()
		{
			return "GoldyStand";
		}

		public const int ATTACK_COUNT = 10;

		public const float EXTRA_GOLD_RATE_BASE = 1f;

		public const float EXTRA_GOLD_RATE_LEVEL = 0.2f;
	}
}
