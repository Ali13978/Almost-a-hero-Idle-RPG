using System;

namespace Simulation
{
	public class CharmEffectStaryStaryDay : CharmEffectData
	{
		public CharmEffectStaryStaryDay()
		{
			CharmDataBase baseData = new CharmDataBase
			{
				charmType = CharmType.Defense,
				id = 208,
				maxLevel = 20,
				dropWeight = 32f,
				nameKey = "CHARM_STARY_STARY_DAY_NAME",
				conditionKey = "CHARM_CONDITION_HERO_CRIT_HITS",
				descKey = "CHARM_STARY_STARY_DAY_DESC"
			};
			base.BaseData = baseData;
		}

		public override void Apply(ChallengeRift chalenge)
		{
			chalenge.AddCharmBuff(new CharmBuffStaryStaryDay
			{
				enchantmentData = this,
				healAmountEachStar = this.GetHeal(this.level),
				totalNumStars = this.GetNumStars(this.level),
				pic = 0.06666667f
			});
		}

		public override string GetDesc(bool showUpgrade)
		{
			string str = string.Empty;
			string str2 = string.Empty;
			if (showUpgrade)
			{
				int num = this.GetNumStars(this.level + 1) - this.GetNumStars(this.level);
				if (num > 0)
				{
					str = AM.cdu(" (+" + num.ToString() + ")");
				}
				float num2 = this.GetHeal(this.level + 1) - this.GetHeal(this.level);
				if (num2 > 0f)
				{
					str2 = AM.cdu(" (+" + GameMath.GetPercentString(num2, false) + ")");
				}
			}
			return string.Format(LM.Get(base.descKey), AM.cds(this.GetNumStars(this.level).ToString()) + str, AM.cds(GameMath.GetPercentString(this.GetHeal(this.level), false)) + str2);
		}

		public override string GetActivationDesc(bool showUpgrade)
		{
			return string.Format(LM.Get(base.conditionKey), AM.cas(15.ToString()));
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
			return 0.28f + 0.02f * (float)(lev - (lev + 1) / 5);
		}

		public int GetNumStars(int lev)
		{
			return 2 + (lev + 1) / 5;
		}

		public override int GetNumPacksRequired()
		{
			return 1;
		}

		public const float BASE_HEAL = 0.28f;

		public const float PER_LEVEL_HEAL = 0.02f;

		public const int BASE_NUM_STARS = 2;

		public const int PER_5_LEVEL_NUM_STARS = 1;

		public const int BASE_ACTIVATE_THRESHOLD = 15;
	}
}
