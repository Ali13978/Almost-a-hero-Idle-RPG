using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectProtectedShield : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataProtectedShield buffDataProtectedShield = new BuffDataProtectedShield(0.10000000149011612 + 0.029999999329447746 * (double)this.level);
			buffDataProtectedShield.id = 249;
			buffDataProtectedShield.isPermenant = true;
			buffDataList.Add(buffDataProtectedShield);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.10000000149011612 + (double)lev * 0.029999999329447746, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.029999999329447746, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_PROTECTED_SHIELD"), text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.10000000149011612, false);
			return string.Format(LM.Get("TRINKET_EFFECT_PROTECTED_SHIELD"), percentString);
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
				UiData.inst.spriteTrinketHangers[1]
			};
		}

		public override TrinketEffectGroup GetGroup()
		{
			return TrinketEffectGroup.SECONDARY;
		}

		public override string GetDebugName()
		{
			return "Protected Shield";
		}

		public override int GetMaxLevel()
		{
			return 10;
		}

		public const double DEFENSE_BASE = 0.10000000149011612;

		public const double DEFENSE_LEVEL = 0.029999999329447746;
	}
}
