using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectBolt : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataBolt buffDataBolt = new BuffDataBolt(5.0 + 0.5 * (double)this.level, 15f);
			buffDataBolt.id = 244;
			buffDataBolt.isPermenant = true;
			buffDataList.Add(buffDataBolt);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(5.0 + (double)lev * 0.5, false));
			string arg = base.csg(GameMath.GetTimeInMilliSecondsString(15f));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.5, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_BOLT"), text, arg);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(5.0, false);
			string timeInMilliSecondsString = GameMath.GetTimeInMilliSecondsString(15f);
			return string.Format(LM.Get("TRINKET_EFFECT_BOLT"), percentString, timeInMilliSecondsString);
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
				UiData.inst.spriteTrinketIcons[17]
			};
		}

		public override TrinketEffectGroup GetGroup()
		{
			return TrinketEffectGroup.SPECIAL;
		}

		public override int GetMaxLevel()
		{
			return 20;
		}

		public override string GetDebugName()
		{
			return "Bolt";
		}

		public const double DMG_BASE = 5.0;

		public const double DMG_LEVEL = 0.5;

		public const float PERIOD = 15f;
	}
}
