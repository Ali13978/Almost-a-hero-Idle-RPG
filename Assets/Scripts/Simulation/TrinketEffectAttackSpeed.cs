using System;
using System.Collections.Generic;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class TrinketEffectAttackSpeed : TrinketEffect
	{
		protected override void InitBuffData(ref List<BuffData> buffDataList)
		{
			BuffDataAttackSpeed buffDataAttackSpeed = new BuffDataAttackSpeed();
			buffDataAttackSpeed.id = 199;
			buffDataAttackSpeed.attackSpeedAdd = 0.2f + 0.04f * (float)this.level;
			buffDataAttackSpeed.isPermenant = true;
			buffDataList.Add(buffDataAttackSpeed);
		}

		public override string GetDesc(bool withUpgrade, int lev = -1)
		{
			if (lev == -1)
			{
				lev = this.level;
			}
			string text = base.csg(GameMath.GetPercentString(0.2f + (float)lev * 0.04f, false));
			if (withUpgrade)
			{
				text += base.csg(" (+" + GameMath.GetPercentString(0.04f, false) + ")");
			}
			return string.Format(LM.Get("TRINKET_EFFECT_ATTACK_SPEED"), text);
		}

		public override string GetDescFirstWithoutColor()
		{
			string percentString = GameMath.GetPercentString(0.2f, false);
			return string.Format(LM.Get("TRINKET_EFFECT_ATTACK_SPEED"), percentString);
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
				UiData.inst.spriteTrinketBeads[0]
			};
		}

		public override TrinketEffectGroup GetGroup()
		{
			return TrinketEffectGroup.COMMON;
		}

		public override string GetDebugName()
		{
			return "Attack Speed";
		}

		public override int GetMaxLevel()
		{
			return 20;
		}

		public const float SPEED_BASE = 0.2f;

		public const float SPEED_LEVEL = 0.04f;
	}
}
