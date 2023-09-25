using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectOffenceDeffence : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataOffenceDeffence buffDataOffenceDeffence = new BuffDataOffenceDeffence(20, 0.05f + 0.02f * (float)this.level);
			buffDataOffenceDeffence.id = 295;
			buffDataOffenceDeffence.isPermenant = true;
			buffDataList.Add(buffDataOffenceDeffence);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string arg = base.csg(20.ToString());
			string text = base.csg(GameMath.GetPercentString(0.05f + (float)lev * 0.02f, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.02f, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_OFFENCE_DEFFENCE"), arg, text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string arg = 20.ToString();
			string percentString = GameMath.GetPercentString(0.05f, false);
			return string.Format(LM.Get("TRINKET_EFFECT_OFFENCE_DEFFENCE"), arg, percentString);
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
				UiData.inst.spriteTrinketHangers[14]
			};
		}

		public override TrinketEffectGroup GetGroup()
		{
			return TrinketEffectGroup.SECONDARY;
		}

		public override int GetMaxLevel()
		{
			return 15;
		}

		public override string GetDebugName()
		{
			return "OffenceDeffence";
		}

		public const int TRIGGER_COUNT = 20;

		public const float SHIELD_AMOUNT_BASE = 0.05f;

		public const float SHIELD_AMOUNT_LEVEL = 0.02f;
	}
}
