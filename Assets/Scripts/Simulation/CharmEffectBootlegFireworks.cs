using System;

namespace Simulation
{
	public class CharmEffectBootlegFireworks : CharmEffectData
	{
		public CharmEffectBootlegFireworks()
		{
			CharmDataBase baseData = new CharmDataBase
			{
				charmType = CharmType.Attack,
				id = 102,
				maxLevel = 20,
				dropWeight = 32f,
				nameKey = "CHARM_BOOTLEG_FIREWORKS_NAME",
				conditionKey = "CHARM_CONDITION_STUN_ENEMIES",
				descKey = "CHARM_BOOTLEG_FIREWORKS_DESC"
			};
			base.BaseData = baseData;
		}

		public override void Apply(ChallengeRift chalenge)
		{
			CharmBuffBootlegFireworks charmBuffBootlegFireworks = new CharmBuffBootlegFireworks();
			charmBuffBootlegFireworks.enchantmentData = this;
			charmBuffBootlegFireworks.totalNumTimes = this.GetTotalTimes(this.level);
			charmBuffBootlegFireworks.teamDamageToDeal = this.GetDamageAmount(this.level);
			charmBuffBootlegFireworks.durActive = (float)charmBuffBootlegFireworks.totalNumTimes * CharmBuffBootlegFireworks.HIT_TIME_DELAY + 2f;
			charmBuffBootlegFireworks.pic = 0.2f;
			chalenge.AddCharmBuff(charmBuffBootlegFireworks);
		}

		public override string GetDesc(bool showUpgrade)
		{
			string str = string.Empty;
			string str2 = string.Empty;
			if (showUpgrade)
			{
				double num = this.GetDamageAmount(this.level + 1) - this.GetDamageAmount(this.level);
				if (num > 0.0)
				{
					str = AM.cdu(" (+" + GameMath.GetPercentString(num, false) + ")");
				}
				int num2 = this.GetTotalTimes(this.level + 1) - this.GetTotalTimes(this.level);
				if (num2 > 0)
				{
					str2 = AM.cdu(" (+" + num2.ToString() + ")");
				}
			}
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetDamageAmount(this.level), false)) + str, AM.cds(this.GetTotalTimes(this.level).ToString()) + str2);
		}

		public override string GetActivationDesc(bool showUpgrade)
		{
			return string.Format(LM.Get(base.conditionKey), AM.cas(5.ToString()));
		}

		public override string GetDesc()
		{
			return this.GetDesc(false);
		}

		public override string GetConditionDescription()
		{
			return this.GetActivationDesc(false);
		}

		public double GetDamageAmount(int lev)
		{
			return 1.8 + 0.2 * (double)(lev - (lev + 1) / 5);
		}

		public int GetTotalTimes(int lev)
		{
			return 2 + (lev + 1) / 5;
		}

		public override int GetNumPacksRequired()
		{
			return 40;
		}

		public const double BASE_DAMAGE = 1.8;

		public const double PER_5_LEVEL_DAMAGE = 0.2;

		public const int BASE_NUM = 2;

		public const int PER_5_LEVEL_NUM = 1;

		public const int BASE_ACTIVATE_THRESHOLD = 5;
	}
}
