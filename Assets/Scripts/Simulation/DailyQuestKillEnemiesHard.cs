using System;
using Ui;

namespace Simulation
{
	public class DailyQuestKillEnemiesHard : DailyQuest
	{
		public DailyQuestKillEnemiesHard()
		{
			this.goal = 1800;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 120.0)
			{
				uiState = UiState.HUB_ACHIEVEMENTS
			};
		}

		public override void OnKilledEnemy(int count)
		{
			this.progress = GameMath.GetMinInt(this.progress + count, this.goal);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 0.8f;
		}

		public override string GetQuestString()
		{
			return LM.Get("DAILY_QUEST_KILL_ENEMIES");
		}

		public override bool IsAvailable(Simulator sim)
		{
			return true;
		}
	}
}
