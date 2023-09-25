using System;

namespace Simulation
{
	public class CharmEffectSpellShield : CharmEffectData
	{
		public CharmEffectSpellShield()
		{
			CharmDataBase baseData = new CharmDataBase
			{
				charmType = CharmType.Defense,
				id = 205,
				maxLevel = 20,
				dropWeight = 32f,
				nameKey = "CHARM_NINJA_SHIELD_NAME",
				conditionKey = "CHARM_CONDITION_ABILITIES_CAST",
				descKey = "CHARM_NINJA_SHIELD_DESC"
			};
			base.BaseData = baseData;
		}

		public override void Apply(ChallengeRift chalenge)
		{
			chalenge.AddCharmBuff(new CharmBuffSpellShield
			{
				enchantmentData = this,
				healthAdd = this.GetShield(this.level),
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
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetShield(this.level), false)) + str);
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
			return 0.18f + 0.02f * (float)(lev - (lev + 1) / 5);
		}

		public int GetThreshold(int lev)
		{
			return 6 - (lev + 1) / 5;
		}

		public override int GetNumPacksRequired()
		{
			return 0;
		}

		public const float BASE_SHIELD = 0.18f;

		public const float PER_LEVEL_SHIELD = 0.02f;

		public const int BASE_ACTIVATE_THRESHOLD = 6;

		public const int PER_5_LEVEL_ACTIVATE_THRESHOLD = 1;
	}
}
