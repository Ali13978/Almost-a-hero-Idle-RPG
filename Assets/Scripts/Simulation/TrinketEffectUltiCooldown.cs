using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectUltiCooldown : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataUltiCooldownTE buffDataUltiCooldownTE = new BuffDataUltiCooldownTE(20f + 2f * (float)this.level);
			buffDataUltiCooldownTE.id = 226;
			buffDataUltiCooldownTE.isPermenant = true;
			buffDataList.Add(buffDataUltiCooldownTE);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(20f + (float)lev * 2f, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(2f, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_ULTI_COOLDOWN"), text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(20f, false);
			return string.Format(LM.Get("TRINKET_EFFECT_ULTI_COOLDOWN"), percentString);
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
			return "Ulti CD";
		}

		public const float DECREASE_BASE = 20f;

		public const float DECREASE_LEVEL = 2f;
	}
}
