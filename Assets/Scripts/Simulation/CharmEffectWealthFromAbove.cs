using System;

namespace Simulation
{
	public class CharmEffectWealthFromAbove : CharmEffectData
	{
		public CharmEffectWealthFromAbove()
		{
			CharmDataBase baseData = new CharmDataBase
			{
				charmType = CharmType.Utility,
				id = 307,
				maxLevel = 20,
				dropWeight = 32f,
				nameKey = "CHARM_WEALTH_FROM_ABOVE_NAME",
				conditionKey = "CHARM_CONDITION_SHIELD_HEROES",
				descKey = "CHARM_WEALTH_FROM_ABOVE_DESC"
			};
			base.BaseData = baseData;
		}

		public override void Apply(ChallengeRift chalenge)
		{
			chalenge.AddCharmBuff(new CharmBuffWealthFromAbove
			{
				enchantmentData = this,
				goldAmount = this.GetGoldAmount(this.level),
				totalNumCoins = this.GetNumCoins(this.level),
				pic = 0.125f
			});
		}

		public override string GetDesc(bool showUpgrade)
		{
			string str = string.Empty;
			string str2 = string.Empty;
			if (showUpgrade)
			{
				int num = this.GetNumCoins(this.level + 1) - this.GetNumCoins(this.level);
				if (num > 0)
				{
					str = AM.cdu(" (+" + num.ToString() + ")");
				}
				double num2 = this.GetGoldAmount(this.level + 1) - this.GetGoldAmount(this.level);
				if (num2 > 0.0)
				{
					str2 = AM.cdu(" (+" + GameMath.GetPercentString(num2, false) + ")");
				}
			}
			return string.Format(LM.Get(base.descKey), AM.cds(this.GetNumCoins(this.level).ToString()) + str, AM.cds(GameMath.GetPercentString(this.GetGoldAmount(this.level), false)) + str2);
		}

		public override string GetActivationDesc(bool showUpgrade)
		{
			return string.Format(LM.Get(base.conditionKey), AM.cas(8.ToString()));
		}

		public override string GetDesc()
		{
			return this.GetDesc(false);
		}

		public override string GetConditionDescription()
		{
			return this.GetActivationDesc(false);
		}

		public double GetGoldAmount(int lev)
		{
			return 0.19 + 0.01 * (double)(lev - (lev + 1) / 5);
		}

		public int GetNumCoins(int lev)
		{
			return 9 + 2 * ((lev + 1) / 5);
		}

		public override int GetNumPacksRequired()
		{
			return 1;
		}

		public const double BASE_GOLD = 0.19;

		public const double PER_LEVEL_GOLD = 0.01;

		public const int BASE_NUM_COINS = 9;

		public const int PER_5_LEVEL_NUM_COINS = 2;

		public const int BASE_ACTIVATE_THRESHOLD = 8;
	}
}
