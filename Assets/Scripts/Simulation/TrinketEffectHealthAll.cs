using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectHealthAll : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataHealthMaxAllTE buffDataHealthMaxAllTE = new BuffDataHealthMaxAllTE();
			buffDataHealthMaxAllTE.id = 261;
			buffDataHealthMaxAllTE.healthMaxFactorAdd = 0.5 + 0.1 * (double)this.level;
			buffDataHealthMaxAllTE.isPermenant = true;
			buffDataList.Add(buffDataHealthMaxAllTE);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.5 + (double)lev * 0.1, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.1, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_HEALTH_ALL"), text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.5, false);
			return string.Format(LM.Get("TRINKET_EFFECT_HEALTH_ALL"), percentString);
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
				UiData.inst.spriteTrinketBeads[0],
				UiData.inst.spriteTrinketBeads[1],
				UiData.inst.spriteTrinketBeads[0]
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
			return "Health All";
		}

		public const double HEALTH_BASE = 0.5;

		public const double HEALTH_LEVEL = 0.1;
	}
}
