using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectRegeneration : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataHealthRegen buffDataHealthRegen = new BuffDataHealthRegen();
			buffDataHealthRegen.id = 251;
			buffDataHealthRegen.healthRegenAdd = 0.004999999888241291 + 0.0010000000474974513 * (double)this.level;
			buffDataHealthRegen.isPermenant = true;
			buffDataList.Add(buffDataHealthRegen);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.004999999888241291 + (double)lev * 0.0010000000474974513, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.0010000000474974513, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_REGENERATION"), text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.004999999888241291, false);
			return string.Format(LM.Get("TRINKET_EFFECT_REGENERATION"), percentString);
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
				UiData.inst.spriteTrinketHangers[3]
			};
		}

		public override TrinketEffectGroup GetGroup()
		{
			return TrinketEffectGroup.SECONDARY;
		}

		public override string GetDebugName()
		{
			return "Regeneration";
		}

		public override int GetMaxLevel()
		{
			return 10;
		}

		public const double HEALTH_REGEN_BASE = 0.004999999888241291;

		public const double HEALTH_REGEN_LEVEL = 0.0010000000474974513;
	}
}
