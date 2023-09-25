using System;
using Ui;

namespace Simulation
{
	public class DailyQuestHiltCriticalHit : DailyQuest
	{
		public DailyQuestHiltCriticalHit()
		{
			this.goal = 1000;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 95.0)
			{
				uiState = UiState.HUB_ACHIEVEMENTS
			};
		}

		public override void OnHiltCriticalHit(int count)
		{
			this.progress = GameMath.GetMinInt(this.progress + count, this.goal);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 1f;
		}

		public override string GetQuestString()
		{
			return string.Format(LM.Get("DAILY_QUEST_HILT_CRITICAL_HIT"), LM.Get("HERO_NAME_HILT"));
		}

		public override bool IsAvailable(Simulator sim)
		{
			return true;
		}
	}
}
