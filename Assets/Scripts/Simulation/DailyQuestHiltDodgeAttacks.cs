using System;
using Ui;

namespace Simulation
{
	public class DailyQuestHiltDodgeAttacks : DailyQuest
	{
		public DailyQuestHiltDodgeAttacks()
		{
			this.goal = 300;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 90.0)
			{
				uiState = UiState.HUB_ACHIEVEMENTS
			};
		}

		public override void OnHiltDodge()
		{
			this.progress = GameMath.GetMinInt(this.progress + 1, this.goal);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 1f;
		}

		public override string GetQuestString()
		{
			return string.Format(LM.Get("DAILY_QUEST_HILT_DODGE"), LM.Get("HERO_NAME_HILT"));
		}

		public override bool IsAvailable(Simulator sim)
		{
			return true;
		}
	}
}
