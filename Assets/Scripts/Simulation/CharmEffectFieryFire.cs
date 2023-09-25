using System;

namespace Simulation
{
	public class CharmEffectFieryFire : CharmEffectData
	{
		public CharmEffectFieryFire()
		{
			CharmDataBase baseData = new CharmDataBase
			{
				charmType = CharmType.Attack,
				id = 101,
				maxLevel = 20,
				dropWeight = 32f,
				nameKey = "CHARM_FIERY_FIRE_NAME",
				conditionKey = "CHARM_CONDITION_PASS_WAVES",
				descKey = "CHARM_FIERY_FIRE_DESC"
			};
			base.BaseData = baseData;
		}

		public override void Apply(ChallengeRift chalenge)
		{
			chalenge.AddCharmBuff(new CharmBuffFieryFire
			{
				enchantmentData = this,
				numFireballs = this.GetNumFireballs(this.level),
				fireballDamage = this.GetDamagePerFireball(this.level),
				defReduction = this.GetDefenseReduction(this.level),
				pic = 0.333333343f
			});
		}

		public override string GetDesc(bool showUpgrade)
		{
			string str = string.Empty;
			string str2 = string.Empty;
			if (showUpgrade)
			{
				int num = this.GetNumFireballs(this.level + 1) - this.GetNumFireballs(this.level);
				if (num > 0)
				{
					str = AM.cdu(" (+" + num.ToString() + ")");
				}
				double num2 = this.GetDamagePerFireball(this.level + 1) - this.GetDamagePerFireball(this.level);
				if (num2 > 0.0)
				{
					str2 = AM.cdu(" (+" + GameMath.GetPercentString(num2, false) + ")");
				}
			}
			return string.Format(LM.Get(base.descKey), AM.cds(this.GetNumFireballs(this.level).ToString()) + str, AM.cds(GameMath.GetPercentString(this.GetDamagePerFireball(this.level), false)) + str2, AM.cds(GameMath.GetPercentString(this.GetDefenseReduction(this.level), false)));
		}

		public override string GetActivationDesc(bool showUpgrade)
		{
			return string.Format(LM.Get(base.conditionKey), AM.cas(3.ToString()));
		}

		public override string GetDesc()
		{
			return this.GetDesc(false);
		}

		public override string GetConditionDescription()
		{
			return this.GetActivationDesc(false);
		}

		private float GetDefenseReduction(int lev)
		{
			return 0.3f;
		}

		public int GetNumFireballs(int lev)
		{
			return 3 + (lev + 1) / 5;
		}

		public double GetDamagePerFireball(int lev)
		{
			return (double)(2.25f + 0.25f * (float)(lev - (lev + 1) / 5));
		}

		public override int GetNumPacksRequired()
		{
			return 0;
		}

		public const int BASE_NUM_FIREBALL = 3;

		public const int PER_5_LEV_NUM_FIREBALL = 1;

		public const float BASE_DAMAGE = 2.25f;

		public const float PER_LEVEL_DAMAGE = 0.25f;

		public const int BASE_ACTIVATE_THRESHOLD = 3;

		public const float BASE_DEF_REDUCTION = 0.3f;
	}
}
