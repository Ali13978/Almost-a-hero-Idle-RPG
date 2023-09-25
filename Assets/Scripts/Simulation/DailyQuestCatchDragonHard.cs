using System;
using Ui;

namespace Simulation
{
	public class DailyQuestCatchDragonHard : DailyQuest
	{
		public DailyQuestCatchDragonHard()
		{
			this.goal = 20;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 140.0)
			{
				uiState = UiState.HUB_ACHIEVEMENTS
			};
		}

		public override void OnCatchDragon(int count)
		{
			this.progress = GameMath.GetMinInt(this.progress + count, this.goal);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 0.5f;
		}

		public override string GetQuestString()
		{
			return LM.Get("DAILY_QUEST_CATCH_DRAGON");
		}

		public override bool IsAvailable(Simulator sim)
		{
			return true;
		}
	}
}
