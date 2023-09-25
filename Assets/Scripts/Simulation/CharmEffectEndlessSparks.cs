using System;

namespace Simulation
{
	public class CharmEffectEndlessSparks : CharmEffectData
	{
		public CharmEffectEndlessSparks()
		{
			CharmDataBase baseData = new CharmDataBase
			{
				charmType = CharmType.Utility,
				id = 302,
				maxLevel = 20,
				dropWeight = 32f,
				nameKey = "CHARM_ENDLESS_SPARKS_NAME",
				conditionKey = "CHARM_CONDITION_RIFT_SECONDS",
				descKey = "CHARM_ENDLESS_SPARKS_DESC"
			};
			base.BaseData = baseData;
		}

		public override void Apply(ChallengeRift chalenge)
		{
			chalenge.AddCharmBuff(new CharmBuffEndlessSparks
			{
				enchantmentData = this,
				progressMin = this.GetProgressMin(),
				progressMax = this.GetProgressMax(this.level),
				pic = 1f / (float)this.GetActivateThreshold(this.level)
			});
		}

		public override string GetDesc(bool showUpgrade)
		{
			string str = string.Empty;
			if (showUpgrade)
			{
				float num = this.GetProgressMax(this.level + 1) - this.GetProgressMax(this.level);
				if (num > 0f)
				{
					str = AM.cdu(" (+" + GameMath.GetPercentString(num, false) + ")");
				}
			}
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(this.GetProgressMin(), false)), AM.cds(GameMath.GetPercentString(this.GetProgressMax(this.level), false)) + str, LM.Get(base.nameKey));
		}

		public override string GetActivationDesc(bool showUpgrade)
		{
			string str = string.Empty;
			if (showUpgrade)
			{
				int num = this.GetActivateThreshold(this.level) - this.GetActivateThreshold(this.level + 1);
				if (num > 0)
				{
					str = AM.cau(" (-" + num.ToString() + ")");
				}
			}
			return string.Format(LM.Get(base.conditionKey), AM.cas(this.GetActivateThreshold(this.level).ToString()) + str);
		}

		public override string GetDesc()
		{
			return this.GetDesc(false);
		}

		public override string GetConditionDescription()
		{
			return this.GetActivationDesc(false);
		}

		public float GetProgressMin()
		{
			return 0.25f;
		}

		public float GetProgressMax(int lev)
		{
			return 0.4f + 0.1f * (float)((lev + 1) / 5);
		}

		public int GetActivateThreshold(int lev)
		{
			return 52 - 2 * (lev - (lev + 1) / 5);
		}

		public override int GetNumPacksRequired()
		{
			return 25;
		}

		public const float BASE_PROGRESS_MIN = 0.25f;

		public const float BASE_PROGRESS_MAX = 0.4f;

		public const float PER_5_LEVEL_PROGRESS_MAX = 0.1f;

		public const int BASE_ACTIVATE_THRESHOLD = 52;

		public const int PER_LEVEL_THRESHOLD = 2;
	}
}
