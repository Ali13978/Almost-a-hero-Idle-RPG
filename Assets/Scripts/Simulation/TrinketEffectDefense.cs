using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectDefense : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataDefense buffDataDefense = new BuffDataDefense();
			buffDataDefense.id = 62;
			buffDataDefense.isPermenant = true;
			buffDataDefense.damageTakenFactor = (double)(1f - (0.2f + 0.02f * (float)this.level));
			buffDataList.Add(buffDataDefense);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.2f + (float)lev * 0.02f, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.02f, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_DEFENSE"), text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.2f, false);
			return string.Format(LM.Get("TRINKET_EFFECT_DEFENSE"), percentString);
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
			return "Defense";
		}

		public const float DEFENSE_BASE = 0.2f;

		public const float DEFENSE_LEVEL = 0.02f;
	}
}
