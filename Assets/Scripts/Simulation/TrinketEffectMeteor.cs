using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectMeteor : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataMeteor buffDataMeteor = new BuffDataMeteor(1.0 + 0.2 * (double)this.level, 8f);
			buffDataMeteor.id = 208;
			buffDataMeteor.isPermenant = true;
			buffDataList.Add(buffDataMeteor);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(1.0 + (double)lev * 0.2, false));
			string arg = base.csg(GameMath.GetTimeInMilliSecondsString(8f));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.2, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_METEOR"), text, arg);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(1.0, false);
			string timeInMilliSecondsString = GameMath.GetTimeInMilliSecondsString(8f);
			return string.Format(LM.Get("TRINKET_EFFECT_METEOR"), percentString, timeInMilliSecondsString);
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
				UiData.inst.spriteTrinketIcons[21]
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
			return "Meteor";
		}

		public const double DMG_BASE = 1.0;

		public const double DMG_LEVEL = 0.2;

		public const float PERIOD = 8f;
	}
}
