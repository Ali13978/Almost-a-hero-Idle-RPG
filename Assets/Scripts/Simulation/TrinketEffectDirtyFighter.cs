using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectDirtyFighter : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataDirtyFighter buffDataDirtyFighter = new BuffDataDirtyFighter((double)(1.5f + 0.1f * (float)this.level));
			buffDataDirtyFighter.id = 257;
			buffDataDirtyFighter.isPermenant = true;
			buffDataList.Add(buffDataDirtyFighter);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.5f + (float)lev * 0.1f, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.1f, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_DIRTY_FIGHTER"), text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.5f, false);
			return string.Format(LM.Get("TRINKET_EFFECT_DIRTY_FIGHTER"), percentString);
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
				UiData.inst.spriteTrinketHangers[9]
			};
		}

		public override TrinketEffectGroup GetGroup()
		{
			return TrinketEffectGroup.SECONDARY;
		}

		public override string GetDebugName()
		{
			return "Dirty Fighter";
		}

		public override int GetMaxLevel()
		{
			return 15;
		}

		public const float DAMAGE_FACTOR_ADD_BASE = 0.5f;

		public const float DAMAGE_FACTOR_ADD_LEVEL = 0.1f;
	}
}
