using System;

namespace Simulation
{
	public class CharmEffectTimeWarper : CharmEffectData
	{
		public CharmEffectTimeWarper()
		{
			CharmDataBase baseData = new CharmDataBase
			{
				charmType = CharmType.Utility,
				id = 309,
				maxLevel = 20,
				dropWeight = 32f,
				nameKey = "CHARM_TIME_WARPER_NAME",
				descKey = "CHARM_TIME_WARPER_DESC",
				isAlwaysActive = true
			};
			base.BaseData = baseData;
		}

		public override void Apply(ChallengeRift chalenge)
		{
			chalenge.AddCharmBuff(new CharmBuffTimeWarper
			{
				enchantmentData = this,
				timeFactor = 1f + this.GetTimeFactor(this.level)
			});
		}

		public override string GetDesc(bool showUpgrade)
		{
			string str = string.Empty;
			if (showUpgrade)
			{
				float num = this.GetTimeFactor(this.level + 1) - this.GetTimeFactor(this.level);
				if (num > 0f)
				{
					str = AM.cdu(" (+" + GameMath.GetPercentString(num, false) + ")");
				}
			}
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetTimeFactor(this.level), false)) + str);
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

		public float GetTimeFactor(int lev)
		{
			return 0.23f + 0.03f * (float)(lev + 1);
		}

		public override int GetNumPacksRequired()
		{
			return 90;
		}

		public const float BASE_TIME_FACTOR = 0.23f;

		public const float PER_LEVEL_TIME_FACTOR = 0.03f;
	}
}
