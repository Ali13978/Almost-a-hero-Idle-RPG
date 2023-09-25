using System;
using Ui;

namespace Simulation
{
	public class DailyQuestPassStagesEasy : DailyQuest
	{
		public DailyQuestPassStagesEasy()
		{
			this.goal = 150;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 80.0)
			{
				uiState = UiState.HUB_ACHIEVEMENTS
			};
		}

		public override void OnPassStage(int count)
		{
			this.progress = GameMath.GetMinInt(this.progress + count, this.goal);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 1f;
		}

		public override string GetQuestString()
		{
			return LM.Get("DAILY_QUEST_PASS_STAGES");
		}

		public override bool IsAvailable(Simulator sim)
		{
			return true;
		}
	}
}
