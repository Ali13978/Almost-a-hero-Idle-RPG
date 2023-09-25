using System;
using Ui;

namespace Simulation
{
	public class DailyQuestCatchDragonEasy : DailyQuest
	{
		public DailyQuestCatchDragonEasy()
		{
			this.goal = 8;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 70.0)
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
			return 1f;
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
