using System;

namespace Simulation
{
	public class CharmEffectBouncingBolt : CharmEffectData
	{
		public CharmEffectBouncingBolt()
		{
			CharmDataBase baseData = new CharmDataBase
			{
				charmType = CharmType.Attack,
				id = 109,
				maxLevel = 20,
				dropWeight = 32f,
				nameKey = "CHARM_BOUNCING_BOLT_NAME",
				conditionKey = "CHARM_CONDITION_HERO_ATTACKS",
				descKey = "CHARM_BOUNCING_BOLT_DESC"
			};
			base.BaseData = baseData;
		}

		public override void Apply(ChallengeRift chalenge)
		{
			chalenge.AddCharmBuff(new CharmBuffBouncingBolt
			{
				enchantmentData = this,
				totalNumBounce = this.GetNumBounces(this.level),
				teamDamageToDeal = this.GetDamage(this.level),
				pic = 0.06666667f
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
					str2 = AM.cdu(" (+" + GameMath.GetPercentString(num, false) + ")");
				}
				int num2 = this.GetNumBounces(this.level + 1) - this.GetNumBounces(this.level);
				if (num2 > 0)
				{
					str = AM.cdu(" (+" + num2.ToString() + ")");
				}
			}
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetDamage(this.level), false)) + str2, AM.cds(this.GetNumBounces(this.level).ToString()) + str);
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

		public double GetDamage(int lev)
		{
			return 0.85 + 0.15 * (double)(lev - (lev + 1) / 5);
		}

		private int GetNumBounces(int lev)
		{
			return 5 + (lev + 1) / 5;
		}

		public override int GetNumPacksRequired()
		{
			return 0;
		}

		public const double BASE_DAMAGE = 0.85;

		public const double PER_LEVEL_DAMAGE = 0.15;

		public const int BASE_BOUNCE = 5;

		public const int PER_5_LEVEL_BOUNCE = 1;

		public const int BASE_ACTIVATE_THRESHOLD = 15;
	}
}
