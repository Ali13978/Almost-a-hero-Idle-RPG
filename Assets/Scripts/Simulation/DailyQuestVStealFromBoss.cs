using System;
using Ui;

namespace Simulation
{
	public class DailyQuestVStealFromBoss : DailyQuest
	{
		public DailyQuestVStealFromBoss()
		{
			this.goal = 10;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 120.0)
			{
				uiState = UiState.HUB_ACHIEVEMENTS
			};
		}

		public override void OnVStealBoss()
		{
			this.progress = GameMath.GetMinInt(this.progress + 1, this.goal);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 0.55f;
		}

		public override string GetQuestString()
		{
			return string.Format(LM.Get("DAILY_QUEST_V_STEAL_FROM_BOSS"), LM.Get("HERO_NAME_V"));
		}

		public override bool IsAvailable(Simulator sim)
		{
			return sim.IsHeroUnlocked("SHEELA");
		}
	}
}
