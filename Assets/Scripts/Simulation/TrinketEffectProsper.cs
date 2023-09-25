using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectProsper : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataReviveDur buffDataReviveDur = new BuffDataReviveDur();
			buffDataReviveDur.id = 258;
			buffDataReviveDur.reviveDurFactorFactor = 1f - (0.1f + 0.05f * (float)this.level);
			buffDataReviveDur.isPermenant = true;
			buffDataList.Add(buffDataReviveDur);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.1f + (float)lev * 0.05f, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.05f, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_PROSPER"), text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.1f, false);
			return string.Format(LM.Get("TRINKET_EFFECT_PROSPER"), percentString);
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
				UiData.inst.spriteTrinketHangers[10]
			};
		}

		public override TrinketEffectGroup GetGroup()
		{
			return TrinketEffectGroup.SECONDARY;
		}

		public override string GetDebugName()
		{
			return "Prosper";
		}

		public override int GetMaxLevel()
		{
			return 5;
		}

		public const float DURATION_BASE = 0.1f;

		public const float DURATION_LEVEL = 0.05f;
	}
}
