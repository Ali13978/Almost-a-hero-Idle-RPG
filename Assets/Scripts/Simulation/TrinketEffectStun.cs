using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectStun : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataTEStun buffDataTEStun = new BuffDataTEStun(0.05f, 2f + 0.2f * (float)this.level, (double)(0.5f + 0.05f * (float)this.level));
			buffDataTEStun.id = 222;
			buffDataTEStun.isPermenant = true;
			buffDataList.Add(buffDataTEStun);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string arg = base.csg(GameMath.GetPercentString(0.05f, false));
			string text = base.csg(GameMath.GetTimeInMilliSecondsString(2f + (float)lev * 0.2f));
			string text2 = base.csg(GameMath.GetPercentString(0.5f + (float)lev * 0.05f, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetTimeInMilliSecondsString(0.2f) + ")");
				text2 += base.csg(" (+" + GameMath.GetPercentString(0.05f, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_STUN"), arg, text, text2);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.05f, false);
			string timeInMilliSecondsString = GameMath.GetTimeInMilliSecondsString(2f);
			string percentString2 = GameMath.GetPercentString(0.5f, false);
			return string.Format(LM.Get("TRINKET_EFFECT_STUN"), percentString, timeInMilliSecondsString, percentString2);
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
				UiData.inst.spriteTrinketHangers[4]
			};
		}

		public override TrinketEffectGroup GetGroup()
		{
			return TrinketEffectGroup.SECONDARY;
		}

		public override int GetMaxLevel()
		{
			return 10;
		}

		public override string GetDebugName()
		{
			return "Stun";
		}

		public const float CHANCE = 0.05f;

		public const float STUN_DUR_BASE = 2f;

		public const float STUN_DUR_LEVEL = 0.2f;

		public const float DAMAGE_BASE = 0.5f;

		public const float DAMAGE_LEVEL = 0.05f;
	}
}
