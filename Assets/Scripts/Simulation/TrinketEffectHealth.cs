using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectHealth : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataHealthMaxTE buffDataHealthMaxTE = new BuffDataHealthMaxTE();
			buffDataHealthMaxTE.id = 223;
			buffDataHealthMaxTE.healthMaxFactorAdd = 1.0 + 0.2 * (double)this.level;
			buffDataHealthMaxTE.isPermenant = true;
			buffDataList.Add(buffDataHealthMaxTE);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(1.0 + (double)lev * 0.2, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.2, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_HEALTH"), text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(1.0, false);
			return string.Format(LM.Get("TRINKET_EFFECT_HEALTH"), percentString);
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
				UiData.inst.spriteTrinketBeads[1],
				UiData.inst.spriteTrinketBeads[1]
			};
		}

		public override TrinketEffectGroup GetGroup()
		{
			return TrinketEffectGroup.COMMON;
		}

		public override int GetMaxLevel()
		{
			return 20;
		}

		public override string GetDebugName()
		{
			return "Health";
		}

		public const double HEALTH_BASE = 1.0;

		public const double HEALTH_LEVEL = 0.2;
	}
}
