using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectCritCrit : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataCritCrit buffDataCritCrit = new BuffDataCritCrit(0.25f + 0.05f * (float)this.level);
			buffDataCritCrit.id = 255;
			buffDataCritCrit.isPermenant = true;
			buffDataList.Add(buffDataCritCrit);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.25f + (float)lev * 0.05f, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.05f, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_CRITCRIT"), text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.25f, false);
			return string.Format(LM.Get("TRINKET_EFFECT_CRITCRIT"), percentString);
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
				UiData.inst.spriteTrinketHangers[7]
			};
		}

		public override TrinketEffectGroup GetGroup()
		{
			return TrinketEffectGroup.SECONDARY;
		}

		public override string GetDebugName()
		{
			return "Crit Crit";
		}

		public override int GetMaxLevel()
		{
			return 10;
		}

		public const float CRITCRIT_CHANCE_BASE = 0.25f;

		public const float CRITCRIT_CHANCE_LEVEL = 0.05f;
	}
}
