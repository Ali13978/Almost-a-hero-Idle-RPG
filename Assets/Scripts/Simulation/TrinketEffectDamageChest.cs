using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectDamageChest : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataDamageChest buffDataDamageChest = new BuffDataDamageChest();
			buffDataDamageChest.id = 218;
			buffDataDamageChest.isPermenant = true;
			buffDataDamageChest.damageAdd = 1.0 + 0.2 * (double)this.level;
			buffDataList.Add(buffDataDamageChest);
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
			return string.Format(LM.Get("TRINKET_EFFECT_DAMAGE_CHEST"), text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(1.0, false);
			return string.Format(LM.Get("TRINKET_EFFECT_DAMAGE_CHEST"), percentString);
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
			return "Chest Damage";
		}

		public const double DMG_BASE = 1.0;

		public const double DMG_LEVEL = 0.2;
	}
}
