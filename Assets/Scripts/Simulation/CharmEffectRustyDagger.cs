using System;

namespace Simulation
{
	public class CharmEffectRustyDagger : CharmEffectData
	{
		public CharmEffectRustyDagger()
		{
			CharmDataBase baseData = new CharmDataBase
			{
				charmType = CharmType.Defense,
				id = 202,
				maxLevel = 20,
				dropWeight = 32f,
				nameKey = "CHARM_RUSTY_DAGGER_NAME",
				conditionKey = "CHARM_CONDITION_COLLECT_GOLD_PIECES",
				descKey = "CHARM_RUSTY_DAGGER_DESC"
			};
			base.BaseData = baseData;
		}

		public override void Apply(ChallengeRift chalenge)
		{
			chalenge.AddCharmBuff(new CharmBuffRustyDagger
			{
				enchantmentData = this,
				numDaggers = this.GetNumDaggers(this.level),
				damageMul = 1.5,
				damageReduction = 0.4,
				pic = 1f / (float)this.GetActivationReq(this.level)
			});
		}

		public override string GetDesc(bool showUpgrade)
		{
			string str = string.Empty;
			if (showUpgrade)
			{
				int num = this.GetNumDaggers(this.level + 1) - this.GetNumDaggers(this.level);
				if (num > 0)
				{
					str = AM.cdu(" (+" + num.ToString() + ")");
				}
			}
			return string.Format(LM.Get(base.descKey), AM.cds(this.GetNumDaggers(this.level).ToString()) + str, AM.cds(GameMath.GetPercentString(1.5, false)), AM.cds(GameMath.GetPercentString(0.4, false)));
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
			return 78 - 2 * (lev - (lev + 1) / 5);
		}

		public int GetNumDaggers(int lev)
		{
			return 4 + (lev + 1) / 5;
		}

		public override int GetNumPacksRequired()
		{
			return 60;
		}

		public const int BASE_NUM_DAGGERS = 4;

		public const int PER_5_LEVEL_DAGGERS = 1;

		public const double BASE_DAMAGE_MUL = 1.5;

		public const double BASE_DAMAGE_RED = 0.4;

		public const int BASE_ACTIVATE_THRESHOLD = 78;

		public const int PER_LEVEL_ACTIVATE_THRESHOLD = 2;
	}
}
