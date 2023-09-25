using System;

namespace Simulation
{
	public class CharmEffectLucrativeLightning : CharmEffectData
	{
		public CharmEffectLucrativeLightning()
		{
			CharmDataBase baseData = new CharmDataBase
			{
				charmType = CharmType.Utility,
				id = 310,
				maxLevel = 20,
				dropWeight = 32f,
				nameKey = "CHARM_LUCRATIVE_LIGHTNING_NAME",
				conditionKey = "CHARM_CONDITION_HERO_ATTACKS",
				descKey = "CHARM_LUCRATIVE_LIGHTNING_DESC"
			};
			base.BaseData = baseData;
		}

		public override void Apply(ChallengeRift rift)
		{
			rift.AddCharmBuff(new CharmBuffLucrativeLightning
			{
				enchantmentData = this,
				damageMul = this.GetDamage(this.level),
				goldIncrease = this.GetGoldIncrease(this.level),
				durActive = 4f,
				pic = 0.025f
			});
		}

		public override string GetDesc(bool showUpgrade)
		{
			string str = string.Empty;
			string str2 = string.Empty;
			if (showUpgrade)
			{
				double num = this.GetDamage(this.level + 1) - this.GetDamage(this.level);
				if (num > 0.0)
				{
					str = AM.cdu(" (+" + GameMath.GetPercentString(num, false) + ")");
				}
				double num2 = this.GetGoldIncrease(this.level + 1) - this.GetGoldIncrease(this.level);
				if (num2 > 0.0)
				{
					str2 = AM.cdu(" (+" + GameMath.GetPercentString(num2, false) + ")");
				}
			}
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetDamage(this.level), false)) + str, AM.cds(GameMath.GetPercentString(this.GetGoldIncrease(this.level), false)) + str2, AM.cds(GameMath.GetTimeInSecondsString(4f)));
		}

		public override string GetActivationDesc(bool showUpgrade)
		{
			return string.Format(LM.Get(base.conditionKey), AM.cas(40.ToString()));
		}

		public override string GetDesc()
		{
			return this.GetDesc(false);
		}

		public override string GetConditionDescription()
		{
			return this.GetActivationDesc(false);
		}

		public double GetDamage(int lev)
		{
			return 0.4 + 0.1 * (double)(lev - (lev + 1) / 5);
		}

		public double GetGoldIncrease(int lev)
		{
			return 0.3 + 0.1 * (double)((lev + 1) / 5);
		}

		public override int GetNumPacksRequired()
		{
			return 0;
		}

		public const float BASE_DUR = 4f;

		public const double BASE_DAMAGE = 0.4;

		public const double PER_LEVEL_DAMAGE = 0.1;

		public const double BASE_GOLD_INC = 0.3;

		public const double PER_5_LEVEL_GOLD_INC = 0.1;

		public const int BASE_ACTIVATE_THRESHOLD = 40;
	}
}
