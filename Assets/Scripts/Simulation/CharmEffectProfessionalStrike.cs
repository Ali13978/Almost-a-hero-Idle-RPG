using System;

namespace Simulation
{
	public class CharmEffectProfessionalStrike : CharmEffectData
	{
		public CharmEffectProfessionalStrike()
		{
			CharmDataBase baseData = new CharmDataBase
			{
				charmType = CharmType.Attack,
				id = 104,
				maxLevel = 20,
				dropWeight = 32f,
				nameKey = "CHARM_PROFESSIONAL_STRIKE_NAME",
				descKey = "CHARM_PROFESSIONAL_STRIKE_DESC",
				isAlwaysActive = true
			};
			base.BaseData = baseData;
		}

		public override void Apply(ChallengeRift chalenge)
		{
			chalenge.AddCharmBuff(new CharmBuffProfessionalStrike
			{
				enchantmentData = this,
				critFactorIncrease = this.GetCritDamage(this.level)
			});
		}

		public override string GetDesc(bool showUpgrade)
		{
			string str = string.Empty;
			if (showUpgrade)
			{
				float num = this.GetCritDamage(this.level + 1) - this.GetCritDamage(this.level);
				if (num > 0f)
				{
					str = AM.cdu(" (+" + GameMath.GetPercentString(num, false) + ")");
				}
			}
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetCritDamage(this.level), false)) + str);
		}

		public override string GetActivationDesc(bool showUpgrade)
		{
			return string.Empty;
		}

		public override string GetDesc()
		{
			return this.GetDesc(false);
		}

		public override string GetConditionDescription()
		{
			return string.Empty;
		}

		public float GetCritDamage(int lev)
		{
			return 0.58f + 0.03f * (float)lev;
		}

		public override int GetNumPacksRequired()
		{
			return 80;
		}

		public const float BASE_INCREMENTER = 0.58f;

		public const float PER_LEVEL_INCREMENTER = 0.03f;

		public const int BASE_ACTIVATE_THRESHOLD = 5;
	}
}
