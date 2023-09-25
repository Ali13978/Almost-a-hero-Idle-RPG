using System;

namespace Simulation
{
	public class CharmEffectThirstingFiends : CharmEffectData
	{
		public CharmEffectThirstingFiends()
		{
			CharmDataBase baseData = new CharmDataBase
			{
				charmType = CharmType.Attack,
				id = 108,
				maxLevel = 20,
				dropWeight = 32f,
				nameKey = "CHARM_THIRSTING_FIENDS_NAME",
				conditionKey = "CHARM_CONDITION_ACTIVATE_OTHER_CHARMS",
				descKey = "CHARM_THIRSTING_FIENDS_DESC"
			};
			base.BaseData = baseData;
		}

		public override void Apply(ChallengeRift chalenge)
		{
			chalenge.AddCharmBuff(new CharmBuffThirstingFiends
			{
				enchantmentData = this,
				totalDragonsToSummon = 3,
				pic = 1f / (float)this.GetActivateThreshold(this.level),
				damageToHero = 0.25,
				teamDamageMul = this.GetDamage(this.level)
			});
		}

		public override string GetDesc(bool showUpgrade)
		{
			string str = string.Empty;
			if (showUpgrade)
			{
				double num = this.GetDamage(this.level + 1) - this.GetDamage(this.level);
				if (num > 0.0)
				{
					str = AM.cdu(" (+" + GameMath.GetPercentString(num, false) + ")");
				}
			}
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(0.25, false)), AM.cds(3.ToString()), AM.cds(GameMath.GetPercentString(this.GetDamage(this.level), false)) + str);
		}

		public override string GetActivationDesc(bool showUpgrade)
		{
			string str = string.Empty;
			if (showUpgrade)
			{
				int num = this.GetActivateThreshold(this.level) - this.GetActivateThreshold(this.level + 1);
				if (num > 0)
				{
					str = AM.cau(" (-" + num.ToString() + ")");
				}
			}
			return string.Format(LM.Get(base.conditionKey), AM.cas(this.GetActivateThreshold(this.level).ToString()) + str);
		}

		public override string GetDesc()
		{
			return this.GetDesc(false);
		}

		public override string GetConditionDescription()
		{
			return this.GetActivationDesc(false);
		}

		public int GetActivateThreshold(int lev)
		{
			return 9 - (lev + 1) / 5;
		}

		public double GetDamage(int lev)
		{
			return (double)(1.25f + 0.25f * (float)(lev - (lev + 1) / 5));
		}

		public override int GetNumPacksRequired()
		{
			return 20;
		}

		public const float BASE_DAMAGE_MUL = 1.25f;

		public const float PER_LEVEL_DAMAGE_MUL = 0.25f;

		public const int BASE_ACTIVATE_THRESHOLD = 9;

		public const int PER_5_LEVEL_ACTIVATE_THRESHOLD = 1;

		public const int BASE_NUM_DRAGONS = 3;

		public const double DAMAGE_TO_HERO = 0.25;
	}
}
