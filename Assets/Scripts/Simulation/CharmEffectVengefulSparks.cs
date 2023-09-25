using System;

namespace Simulation
{
	public class CharmEffectVengefulSparks : CharmEffectData
	{
		public CharmEffectVengefulSparks()
		{
			CharmDataBase baseData = new CharmDataBase
			{
				charmType = CharmType.Utility,
				id = 304,
				maxLevel = 20,
				dropWeight = 32f,
				nameKey = "CHARM_VENGEFUL_SPARKS_NAME",
				conditionKey = "CHARM_CONDITION_ENEMY_ATTACKS",
				descKey = "CHARM_VENGEFUL_SPARKS_DESC"
			};
			base.BaseData = baseData;
		}

		public override void Apply(ChallengeRift chalenge)
		{
			chalenge.AddCharmBuff(new CharmBuffVengefulSparks
			{
				enchantmentData = this,
				progressAddAmount = 0.3f,
				totalNumCharms = this.GetNumCharms(this.level),
				pic = 1f / (float)this.GetActivationReq(this.level)
			});
		}

		public override string GetDesc(bool showUpgrade)
		{
			string str = string.Empty;
			if (showUpgrade)
			{
				int num = this.GetNumCharms(this.level + 1) - this.GetNumCharms(this.level);
				if (num > 0)
				{
					str = AM.cdu(" (+" + num.ToString() + ")");
				}
			}
			return string.Format(LM.Get(base.descKey), AM.cds(GameMath.GetPercentString(0.3f, false)), AM.cds(this.GetNumCharms(this.level).ToString()) + str);
		}

		public override string GetActivationDesc(bool showUpgrade)
		{
			string str = string.Empty;
			if (showUpgrade)
			{
				int num = this.GetActivationReq(this.level) - this.GetActivationReq(this.level + 1);
				if (num > 0)
				{
					str = AM.cau(" (-" + num.ToString() + ")");
				}
			}
			return string.Format(LM.Get(base.conditionKey), AM.cas(this.GetActivationReq(this.level).ToString()) + str);
		}

		public override string GetDesc()
		{
			return this.GetDesc(false);
		}

		public override string GetConditionDescription()
		{
			return this.GetActivationDesc(false);
		}

		public int GetActivationReq(int lev)
		{
			return 63 - 3 * (lev - (lev + 1) / 5);
		}

		public int GetNumCharms(int lev)
		{
			return 3 + (lev + 1) / 5;
		}

		public override int GetNumPacksRequired()
		{
			return 20;
		}

		public const float BASE_PROGRESS = 0.3f;

		public const int BASE_NUM_CHARMS = 3;

		public const int PER_5_LEVEL_NUM_CHARMS = 1;

		public const int BASE_ACTIVATE_THRESHOLD = 63;

		public const int PER_LEVEL_ACTIVATE_THRESHOLD = 3;
	}
}
