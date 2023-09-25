using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectDamageConsecutive : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataDamageConsecutive buffDataDamageConsecutive = new BuffDataDamageConsecutive();
			buffDataDamageConsecutive.id = 202;
			buffDataDamageConsecutive.isPermenant = true;
			buffDataDamageConsecutive.damageAdd = 0.1 + 0.02 * (double)this.level;
			buffDataDamageConsecutive.maxStack = 10;
			buffDataList.Add(buffDataDamageConsecutive);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.1 + (double)lev * 0.02, false));
			string arg = base.csg(10.ToString());
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.02, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_DAMAGE_CONSECUTIVE"), text, arg);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.1, false);
			string arg = 10.ToString();
			return string.Format(LM.Get("TRINKET_EFFECT_DAMAGE_CONSECUTIVE"), percentString, arg);
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
				UiData.inst.spriteTrinketBeads[1]
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
			return "Damage Consec.";
		}

		public const double DMG_BASE = 0.1;

		public const double DMG_LEVEL = 0.02;

		public const int MAX_STACK = 10;
	}
}
