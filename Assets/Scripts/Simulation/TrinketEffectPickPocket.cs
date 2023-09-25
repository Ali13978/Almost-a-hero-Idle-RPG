using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectPickPocket : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataPickPocket buffDataPickPocket = new BuffDataPickPocket(1.0 + 0.15000000596046448 * (double)this.level, 15f);
			buffDataPickPocket.id = 241;
			buffDataPickPocket.isPermenant = true;
			buffDataList.Add(buffDataPickPocket);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(1.0 + (double)lev * 0.15000000596046448, false));
			string arg = base.csg(GameMath.GetTimeInMilliSecondsString(15f));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.15000000596046448, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_PICK_POCKET"), text, arg);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(1.0, false);
			string timeInMilliSecondsString = GameMath.GetTimeInMilliSecondsString(15f);
			return string.Format(LM.Get("TRINKET_EFFECT_PICK_POCKET"), percentString, timeInMilliSecondsString);
		}

		public override float GetChanceWeight()
		{
			return 1f;
		}

		public override int GetRarity()
		{
			return 5;
		}

		public override Sprite[] GetSprites()
		{
			return new Sprite[]
			{
				UiData.inst.spriteTrinketIcons[13]
			};
		}

		public override TrinketEffectGroup GetGroup()
		{
			return TrinketEffectGroup.SPECIAL;
		}

		public override int GetMaxLevel()
		{
			return 20;
		}

		public override string GetDebugName()
		{
			return "Pick Pocket";
		}

		public const double GOLD_BASE = 1.0;

		public const double GOLD_LEVEL = 0.15000000596046448;

		public const float COOLDOWN_BASE = 15f;
	}
}
