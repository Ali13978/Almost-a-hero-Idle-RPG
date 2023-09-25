using System;

namespace Simulation
{
	public class CharmEffectFrostyStorm : CharmEffectData
	{
		public CharmEffectFrostyStorm()
		{
			CharmDataBase baseData = new CharmDataBase
			{
				charmType = CharmType.Defense,
				id = 206,
				maxLevel = 20,
				dropWeight = 32f,
				nameKey = "CHARM_FROSTY_STORM_NAME",
				conditionKey = "CHARM_CONDITION_RIFT_SECONDS",
				descKey = "CHARM_FROSTY_STORM_DESC"
			};
			base.BaseData = baseData;
		}

		public override void Apply(ChallengeRift chalenge)
		{
			chalenge.AddCharmBuff(new CharmBuffFrostyStorm
			{
				enchantmentData = this,
				numShards = this.GetNumShards(),
				slowDuration = this.GetSlowDuration(),
				slowAmount = 0.5f,
				shardDamage = this.GetDmg(this.level),
				pic = 1f / (float)this.GetThreshold(this.level)
			});
		}

		public override string GetDesc(bool showUpgrade)
		{
			string str = string.Empty;
			if (showUpgrade)
			{
				double num = this.GetDmg(this.level + 1) - this.GetDmg(this.level);
				if (num > 0.0)
				{
					str = AM.cdu(" (+" + GameMath.GetPercentString(num, false) + ")");
				}
			}
			return string.Format(LM.Get(base.descKey), new object[]
			{
				AM.cds(this.GetNumShards().ToString()),
				AM.cds(GameMath.GetPercentString(this.GetDmg(this.level), false)) + str,
				AM.cds(GameMath.GetPercentString(this.GetSlowAmount(), false)),
				AM.cds(GameMath.GetTimeInSecondsString(this.GetSlowDuration()))
			});
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

		public float GetSlowDuration()
		{
			return 4f;
		}

		public int GetNumShards()
		{
			return 3;
		}

		public int GetThreshold(int lev)
		{
			return 12 - (lev + 1) / 5;
		}

		public double GetDmg(int lev)
		{
			return 0.8 + 0.2 * (double)(lev - (lev + 1) / 5);
		}

		public float GetSlowAmount()
		{
			return 0.5f;
		}

		public override int GetNumPacksRequired()
		{
			return 0;
		}

		public const int BASE_NUM_SHARDS = 3;

		public const double BASE_DMG = 0.8;

		public const double PER_LEVEL_DMG = 0.2;

		public const float BASE_SLOW_AMOUNT = 0.5f;

		public const float BASE_SLOW_DUR = 4f;

		public const int BASE_ACTIVATE_THRESHOLD = 12;

		public const int PER_5_LEVEL_ACTIVATE_THRESHOLD = 1;
	}
}
