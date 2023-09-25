using System;

namespace Simulation
{
	public class CharmEffectQuickStudy : CharmEffectData
	{
		public CharmEffectQuickStudy()
		{
			CharmDataBase baseData = new CharmDataBase
			{
				charmType = CharmType.Utility,
				id = 303,
				maxLevel = 20,
				dropWeight = 32f,
				nameKey = "CHARM_QUICK_STUDY_NAME",
				descKey = "CHARM_QUICK_STUDY_DESC",
				isAlwaysActive = true
			};
			base.BaseData = baseData;
		}

		public override void Apply(ChallengeRift chalenge)
		{
			CharmBuffQuickStudy charmBuffQuickStudy = new CharmBuffQuickStudy();
			charmBuffQuickStudy.enchantmentData = this;
			charmBuffQuickStudy.bookCountToDrop = this.GetBookCount(this.level);
			charmBuffQuickStudy.secondPhaseCance = this.GetSecondPhaseChance(this.level);
			charmBuffQuickStudy.progressPerBook = 1f / (float)charmBuffQuickStudy.bookCountToDrop;
			chalenge.AddCharmBuff(charmBuffQuickStudy);
		}

		public override string GetDesc(bool showUpgrade)
		{
			string str = string.Empty;
			string str2 = string.Empty;
			if (showUpgrade)
			{
				int num = this.GetBookCount(this.level + 1) - this.GetBookCount(this.level);
				if (num > 0)
				{
					str = AM.cdu(" (+" + num + ")");
				}
				float num2 = this.GetSecondPhaseChance(this.level + 1) - this.GetSecondPhaseChance(this.level);
				if (num2 > 0f)
				{
					str2 = AM.cdu(" (+" + GameMath.GetPercentString(num2, false) + ")");
				}
			}
			return string.Format(LM.Get(base.descKey), AM.cds(this.GetBookCount(this.level).ToString()) + str, AM.cds(GameMath.GetPercentString(this.GetSecondPhaseChance(this.level), false)) + str2, AM.cds(this.GetBookCount(this.level).ToString()) + str);
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

		private int GetBookCount(int lev)
		{
			return 5 + (lev + 1) / 5;
		}

		private float GetSecondPhaseChance(int lev)
		{
			return 0.12f + 0.05f * (float)(lev - (lev + 1) / 5);
		}

		public override int GetNumPacksRequired()
		{
			return 75;
		}

		public const int BASE_BOOK_COUNT = 5;

		public const int PER_5_LEVEL_BOOK_COUNT = 1;

		public const float BASE_SECOND_PHASE_CHANCE = 0.12f;

		public const float PER_5_SECOND_PHASE_CHANCE = 0.05f;
	}
}
