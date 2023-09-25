using System;

namespace Simulation
{
	public class CharmEffectQuackatoa : CharmEffectData
	{
		public CharmEffectQuackatoa()
		{
			CharmDataBase baseData = new CharmDataBase
			{
				charmType = CharmType.Defense,
				id = 210,
				maxLevel = 20,
				dropWeight = 32f,
				nameKey = "CHARM_QUACKATOA_NAME",
				conditionKey = "CHARM_CONDITION_ACTIVATE_OTHER_CHARMS",
				descKey = "CHARM_QUACKATOA_DESC"
			};
			base.BaseData = baseData;
		}

		public override void Apply(ChallengeRift chalenge)
		{
			chalenge.AddCharmBuff(new CharmBuffQuackatoa
			{
				enchantmentData = this,
				totalNumDucks = this.GetNumDucks(this.level),
				stunDuration = this.GetStunDuration(this.level),
				pic = 1f / (float)this.GetThreshold()
			});
		}

		public override string GetDesc(bool showUpgrade)
		{
			string str = string.Empty;
			string str2 = string.Empty;
			if (showUpgrade)
			{
				int num = this.GetNumDucks(this.level + 1) - this.GetNumDucks(this.level);
				if (num > 0)
				{
					str = AM.cdu(" (+" + num.ToString() + ")");
				}
				float num2 = this.GetStunDuration(this.level + 1) - this.GetStunDuration(this.level);
				if (num2 > 0f)
				{
					str2 = AM.cdu(" (+" + GameMath.GetTimeInMilliSecondsString(num2) + ")");
				}
			}
			return string.Format(LM.Get(base.descKey), AM.cds(this.GetNumDucks(this.level).ToString()) + str, AM.cds(GameMath.GetTimeInMilliSecondsString(this.GetStunDuration(this.level))) + str2);
		}

		public override string GetActivationDesc(bool showUpgrade)
		{
			return string.Format(LM.Get(base.conditionKey), AM.cas(this.GetThreshold().ToString()));
		}

		public override string GetDesc()
		{
			return this.GetDesc(false);
		}

		public override string GetConditionDescription()
		{
			return this.GetActivationDesc(false);
		}

		public float GetStunDuration(int lev)
		{
			return 1.8f + 0.2f * (float)(lev - (lev + 1) / 5);
		}

		public int GetNumDucks(int lev)
		{
			return 4 + (lev + 1) / 5;
		}

		public int GetThreshold()
		{
			return 7;
		}

		public override int GetNumPacksRequired()
		{
			return 40;
		}

		public const int BASE_NUM_DUCKS = 4;

		public const int PER_5_LEVEL_NUM_DUCKS = 1;

		public const float BASE_STUN_DUR = 1.8f;

		public const float PER_LEVEL_STUN_DUR = 0.2f;

		public const int BASE_ACTIVATE_THRESHOLD = 7;
	}
}
