using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectKillsHeal : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataKillsHeal buffDataKillsHeal = new BuffDataKillsHeal();
			buffDataKillsHeal.id = 206;
			buffDataKillsHeal.isPermenant = true;
			buffDataKillsHeal.healRatio = 0.03 + 0.01 * (double)this.level;
			buffDataList.Add(buffDataKillsHeal);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.03 + (double)lev * 0.01, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.01, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_KILLS_HEAL"), text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.03, false);
			return string.Format(LM.Get("TRINKET_EFFECT_KILLS_HEAL"), percentString);
		}

		public override float GetChanceWeight()
		{
			return 1f;
		}

		public override int GetRarity()
		{
			return 4;
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
			return "Kills Heal";
		}

		public const double HEAL_BASE = 0.03;

		public const double HEAL_LEVEL = 0.01;
	}
}
