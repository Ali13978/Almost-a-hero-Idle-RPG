using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectBackForRevenge : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataBackForRevenge buffDataBackForRevenge = new BuffDataBackForRevenge(420f + -60f * (float)this.level);
			buffDataBackForRevenge.id = 247;
			buffDataBackForRevenge.isPermenant = true;
			buffDataList.Add(buffDataBackForRevenge);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetTimeInMilliSecondsString(420f + -60f * (float)this.level));
			if (withUpgrade)
			{
				text += base.csg(" (" + GameMath.GetTimeInMilliSecondsString(-60f) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_BACK_FOR_REVENGE"), text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string timeInMilliSecondsString = GameMath.GetTimeInMilliSecondsString(420f);
			return string.Format(LM.Get("TRINKET_EFFECT_BACK_FOR_REVENGE"), timeInMilliSecondsString);
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
				UiData.inst.spriteTrinketIcons[19]
			};
		}

		public override TrinketEffectGroup GetGroup()
		{
			return TrinketEffectGroup.SPECIAL;
		}

		public override int GetMaxLevel()
		{
			return 5;
		}

		public override string GetDebugName()
		{
			return "BackForRevenge";
		}

		public const float PERIOD_BASE = 420f;

		public const float PERIOD_LEVEL = -60f;
	}
}
