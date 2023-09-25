using System;
using Ui;

namespace Simulation
{
	public class DailyQuestStunEnemies : DailyQuest
	{
		public DailyQuestStunEnemies()
		{
			this.goal = 1000;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 130.0)
			{
				uiState = UiState.HUB_ACHIEVEMENTS
			};
		}

		public override void OnStunnedEnemy(int count)
		{
			this.progress = GameMath.GetMinInt(this.progress + count, this.goal);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 1f;
		}

		public override string GetQuestString()
		{
			return LM.Get("DAILY_QUEST_STUN_ENEMIES");
		}

		public override bool IsAvailable(Simulator sim)
		{
			return true;
		}
	}
}
