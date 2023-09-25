using System;

namespace Simulation
{
	public class CharmEffectBodyBlock : CharmEffectData
	{
		public CharmEffectBodyBlock()
		{
			CharmDataBase baseData = new CharmDataBase
			{
				charmType = CharmType.Defense,
				id = 203,
				maxLevel = 20,
				dropWeight = 32f,
				nameKey = "CHARM_BODY_BLOCK_NAME",
				conditionKey = "CHARM_CONDITION_ENEMY_ATTACKS",
				descKey = "CHARM_BODY_BLOCK_DESC"
			};
			base.BaseData = baseData;
		}

		public override void Apply(ChallengeRift rift)
		{
			rift.AddCharmBuff(new CharmBuffBodyBlock
			{
				enchantmentData = this,
				shieldAmount = this.GetShield(this.level),
				numShields = 5,
				pic = 1f / (float)this.GetThreshold(this.level)
			});
		}

		public override string GetDesc(bool showUpgrade)
		{
			string str = string.Empty;
			if (showUpgrade)
			{
				float num = this.GetShield(this.level + 1) - this.GetShield(this.level);
				if (num > 0f)
				{
					str = AM.cdu(" (+" + GameMath.GetPercentString(num, false) + ")");
				}
			}
			return string.Format(LM.Get(base.descKey), AM.cds(5.ToString()), AM.cds(GameMath.GetPercentString(this.GetShield(this.level), false)) + str);
		}

		public override string GetActivationDesc(bool showUpgrade)
		{
			string str = string.Empty;
			if (showUpgrade)
			{
				int num = this.GetThreshold(this.level) - this.GetThreshold(this.level + 1);
				if (num > 0)
				{
					str = AM.cau(" (-" + num.ToString() + ")");
				}
			}
			return string.Format(LM.Get(base.conditionKey), AM.cas(this.GetThreshold(this.level).ToString()) + str);
		}

		public override string GetDesc()
		{
			return this.GetDesc(false);
		}

		public override string GetConditionDescription()
		{
			return this.GetActivationDesc(false);
		}

		public float GetShield(int lev)
		{
			return 0.15f + 0.05f * (float)((lev + 1) / 5);
		}

		public int GetThreshold(int lev)
		{
			return 31 - (lev - (lev + 1) / 5);
		}

		public override int GetNumPacksRequired()
		{
			return 1;
		}

		public const int BASE_NUM_SHIELDS_THROWN = 5;

		public const float BASE_SHIELD_AMOUNT = 0.15f;

		public const float PER_5_LEVEL_SHIELD_AMOUNT = 0.05f;

		public const int BASE_ACTIVATE_THRESHOLD = 31;

		public const int PER_LEVEL_ACTIVATE_THRESHOLD = 1;
	}
}
