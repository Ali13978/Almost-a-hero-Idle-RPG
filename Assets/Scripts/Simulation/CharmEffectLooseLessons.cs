using System;

namespace Simulation
{
	public class CharmEffectLooseLessons : CharmEffectData
	{
		public CharmEffectLooseLessons()
		{
			CharmDataBase baseData = new CharmDataBase
			{
				charmType = CharmType.Attack,
				id = 107,
				maxLevel = 20,
				dropWeight = 32f,
				nameKey = "CHARM_LOOSE_LESSONS_NAME",
				conditionKey = "CHARM_CONDITION_HERO_HEALTH_RESTORED",
				descKey = "CHARM_LOOSE_LESSONS_DESC"
			};
			base.BaseData = baseData;
		}

		public override void Apply(ChallengeRift chalenge)
		{
			chalenge.AddCharmBuff(new CharmBuffLooseLessons
			{
				enchantmentData = this,
				teamDamageToDeal = this.GetDamage(this.level),
				stunDuration = this.GetEffectDur(),
				pic = 1f / this.GetActivationReq(this.level)
			});
		}

		public override string GetDesc(bool showUpgrade)
		{
			string str = string.Empty;
			if (showUpgrade)
			{
				double num = (double)(this.GetDamage(this.level + 1) - this.GetDamage(this.level));
				if (num > 0.0)
				{
					str = AM.cdu(" (+" + GameMath.GetPercentString(num, false) + ")");
				}
			}
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetDamage(this.level), false)) + str, AM.cds(GameMath.GetTimeInSecondsString(this.GetEffectDur())));
		}

		public override string GetActivationDesc(bool showUpgrade)
		{
			string str = string.Empty;
			if (showUpgrade)
			{
				float num = this.GetActivationReq(this.level) - this.GetActivationReq(this.level + 1);
				if (num > 0f)
				{
					str = AM.cau(" (-" + GameMath.GetPercentString(num, false) + ")");
				}
			}
			return string.Format(LM.Get(base.conditionKey), AM.cas(GameMath.GetPercentString(this.GetActivationReq(this.level), false)) + str);
		}

		public override string GetDesc()
		{
			return this.GetDesc(false);
		}

		public override string GetConditionDescription()
		{
			return this.GetActivationDesc(false);
		}

		public float GetDamage(int lev)
		{
			return 1.7f + 0.3f * (float)(lev - (lev + 1) / 5);
		}

		private float GetActivationReq(int lev)
		{
			return 0.8f - 0.1f * (float)((lev + 1) / 5);
		}

		public float GetEffectDur()
		{
			return 5f;
		}

		public override int GetNumPacksRequired()
		{
			return 5;
		}

		public const float BASE_INCREMENTER = 1.7f;

		public const float PER_LEVEL_INCREMENTER = 0.3f;

		public const float BASE_DUR = 5f;

		public const float BASE_ACTIVATE_THRESHOLD = 0.8f;

		public const float PER_5_LEVEL_ACTIVATE_THRESHOLD = 0.1f;
	}
}
