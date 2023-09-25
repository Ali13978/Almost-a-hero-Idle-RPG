using System;
using Ui;

namespace Simulation
{
	public class DailyQuestLiaStealHealth : DailyQuest
	{
		public DailyQuestLiaStealHealth()
		{
			this.goal = 100;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 90.0)
			{
				uiState = UiState.HUB_ACHIEVEMENTS
			};
		}

		public override void OnLiaStealHealth(int count)
		{
			this.progress = GameMath.GetMinInt(this.progress + count, this.goal);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 0.75f;
		}

		public override string GetQuestString()
		{
			return string.Format(LM.Get("DAILY_QUEST_LIA_STEAL_HEALTH"), LM.Get("HERO_NAME_LIA"));
		}

		public override bool IsAvailable(Simulator sim)
		{
			return sim.IsHeroUnlocked("BLIND_ARCHER");
		}
	}
}
