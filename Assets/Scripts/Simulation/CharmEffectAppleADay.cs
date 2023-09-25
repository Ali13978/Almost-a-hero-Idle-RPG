using System;

namespace Simulation
{
	public class CharmEffectAppleADay : CharmEffectData
	{
		public CharmEffectAppleADay()
		{
			CharmDataBase baseData = new CharmDataBase
			{
				charmType = CharmType.Defense,
				id = 209,
				maxLevel = 20,
				dropWeight = 32f,
				nameKey = "CHARM_APPLE_A_DAY_NAME",
				conditionKey = "CHARM_CONDITION_HERO_ATTACKS",
				descKey = "CHARM_APPLE_A_DAY_DESC"
			};
			base.BaseData = baseData;
		}

		public override void Apply(ChallengeRift chalenge)
		{
			chalenge.AddCharmBuff(new CharmBuffAppleADay
			{
				enchantmentData = this,
				healAmount = this.GetHeal(this.level),
				critHitIncrease = this.GetCritChance(),
				dur = this.GetEffectDur(this.level),
				totalNumHeroes = 5,
				pic = 0.0125f
			});
		}

		public override string GetDesc(bool showUpgrade)
		{
			string str = string.Empty;
			string str2 = string.Empty;
			if (showUpgrade)
			{
				float num = this.GetHeal(this.level + 1) - this.GetHeal(this.level);
				if (num > 0f)
				{
					str = AM.cdu(" (+" + GameMath.GetPercentString(num, false) + ")");
				}
				float num2 = this.GetEffectDur(this.level + 1) - this.GetEffectDur(this.level);
				if (num2 > 0f)
				{
					str2 = AM.cdu(" (+" + GameMath.GetTimeInSecondsString(num2) + ")");
				}
			}
			return string.Format(LM.Get(base.descKey), new object[]
			{
				AM.cds(5.ToString()),
				AM.cds(GameMath.GetPercentString(this.GetHeal(this.level), false)) + str,
				AM.cds(GameMath.GetPercentString(this.GetCritChance(), false)),
				AM.cds(GameMath.GetTimeInSecondsString(this.GetEffectDur(this.level))) + str2
			});
		}

		public override string GetActivationDesc(bool showUpgrade)
		{
			return string.Format(LM.Get(base.conditionKey), AM.cas(80.ToString()));
		}

		public override string GetDesc()
		{
			return this.GetDesc(false);
		}

		public override string GetConditionDescription()
		{
			return this.GetActivationDesc(false);
		}

		public float GetHeal(int lev)
		{
			return 0.16f + 0.04f * (float)(lev - (lev + 1) / 5);
		}

		public float GetEffectDur(int lev)
		{
			return 5f + 1f * (float)((lev + 1) / 5);
		}

		public float GetCritChance()
		{
			return 0.15f;
		}

		public override int GetNumPacksRequired()
		{
			return 0;
		}

		public const int NUM_HEROES_TO_HEAL = 5;

		public const float BASE_HEAL = 0.16f;

		public const float PER_LEVEL_HEAL = 0.04f;

		public const float BASE_CRIT = 0.15f;

		public const float BASE_DUR = 5f;

		public const float PER_5_LEVEL_DUR = 1f;

		public const int BASE_ACTIVATE_THRESHOLD = 80;
	}
}
