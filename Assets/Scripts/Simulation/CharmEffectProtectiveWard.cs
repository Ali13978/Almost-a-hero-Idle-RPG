using System;

namespace Simulation
{
	public class CharmEffectProtectiveWard : CharmEffectData
	{
		public CharmEffectProtectiveWard()
		{
			CharmDataBase baseData = new CharmDataBase
			{
				charmType = CharmType.Defense,
				id = 207,
				maxLevel = 20,
				dropWeight = 32f,
				nameKey = "CHARM_ANGELIC_WARD_NAME",
				descKey = "CHARM_ANGELIC_WARD_DESC",
				isAlwaysActive = true
			};
			base.BaseData = baseData;
		}

		public override void Apply(ChallengeRift chalenge)
		{
			chalenge.AddCharmBuff(new CharmBuffProtectiveWard
			{
				enchantmentData = this,
				damageProtectionFactor = this.GetDamageReductionFactor(this.level)
			});
		}

		public override string GetDesc(bool showUpgrade)
		{
			string str = string.Empty;
			if (showUpgrade)
			{
				float num = this.GetDamageReductionFactor(this.level + 1) - this.GetDamageReductionFactor(this.level);
				if (num > 0f)
				{
					str = AM.cdu(" (+" + GameMath.GetPercentString(num, false) + ")");
				}
			}
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetDamageReductionFactor(this.level), false)) + str);
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

		public float GetDamageReductionFactor(int lev)
		{
			return 0.16f + 0.01f * (float)lev;
		}

		public override int GetNumPacksRequired()
		{
			return 75;
		}

		public const float BASE_PROTECTION = 0.16f;

		public const float PER_LEVEL_PROTECTION = 0.01f;
	}
}
