using System;
using Ui;

namespace Simulation
{
	public class DailyQuestCollectCandy : DailyQuest
	{
		public DailyQuestCollectCandy()
		{
			this.goal = 1000;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 300.0)
			{
				uiState = UiState.HUB_ACHIEVEMENTS
			};
		}

		public override void OnCollectCandy(int count)
		{
			this.progress = GameMath.GetMinInt(this.progress + count, this.goal);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			if (sim.numCandyQuestCompleted < 1)
			{
				return 20f;
			}
			if (sim.numCandyQuestCompleted < 5)
			{
				return 7f;
			}
			if (sim.numCandyQuestCompleted < 10)
			{
				return 3f;
			}
			return 1f;
		}

		public override string GetQuestString()
		{
			return string.Format(LM.Get("DAILY_QUEST_COLLECT_CANDIES"), new object[0]);
		}

		public override bool IsAvailable(Simulator sim)
		{
			return false;
		}
	}
}
