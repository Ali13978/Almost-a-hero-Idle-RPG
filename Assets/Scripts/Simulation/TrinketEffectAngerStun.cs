using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectAngerStun : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataAngerStun buffDataAngerStun = new BuffDataAngerStun(3f + 1f * (float)this.level, 15);
			buffDataAngerStun.id = 293;
			buffDataAngerStun.isPermenant = true;
			buffDataList.Add(buffDataAngerStun);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string arg = base.csg(15.ToString());
			string text = base.csg(GameMath.GetTimeInMilliSecondsString(3f + (float)lev * 1f));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetTimeInMilliSecondsString(1f) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_ANGER_STUN"), text, arg);
		}

		public override string GetDescFirstWithoutColor()
		{
			string arg = 15.ToString();
			string timeInMilliSecondsString = GameMath.GetTimeInMilliSecondsString(3f);
			return string.Format(LM.Get("TRINKET_EFFECT_ANGER_STUN"), timeInMilliSecondsString, arg);
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
				UiData.inst.spriteTrinketIcons[23]
			};
		}

		public override TrinketEffectGroup GetGroup()
		{
			return TrinketEffectGroup.SPECIAL;
		}

		public override int GetMaxLevel()
		{
			return 10;
		}

		public override string GetDebugName()
		{
			return "AngerStun";
		}

		public const int TRIGGER_HIT_COUNT = 15;

		public const float STUN_DUR_BASE = 3f;

		public const float STUN_DUR_LEVEL = 1f;
	}
}
