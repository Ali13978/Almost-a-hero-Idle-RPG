using System;
using Ui;

namespace Simulation
{
	public class DailyQuestUseRonLandCritHit : DailyQuest
	{
		public DailyQuestUseRonLandCritHit()
		{
			this.goal = 500;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 110.0)
			{
				uiState = UiState.HUB_ACHIEVEMENTS
			};
		}

		public override void OnRonLandedCritHit()
		{
			this.progress = GameMath.GetMinInt(this.progress + 1, this.goal);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 1f;
		}

		public override string GetQuestString()
		{
			return string.Format(LM.Get("DAILY_QUEST_HILT_CRITICAL_HIT"), LM.Get("HERO_NAME_RON"));
		}

		public override bool IsAvailable(Simulator sim)
		{
			return sim.IsHeroUnlocked("DRUID");
		}
	}
}
