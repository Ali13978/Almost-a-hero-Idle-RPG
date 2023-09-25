using System;
using Ui;

namespace Simulation
{
	public class DailyQuestBoomerBoom : DailyQuest
	{
		public DailyQuestBoomerBoom()
		{
			this.goal = 60;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 110.0)
			{
				uiState = UiState.HUB_ACHIEVEMENTS
			};
		}

		public override void OnBoomerBoom()
		{
			this.progress = GameMath.GetMinInt(this.progress + 1, this.goal);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 0.7f;
		}

		public override string GetQuestString()
		{
			return string.Format(LM.Get("DAILY_QUEST_BOOMER_BOOM"), LM.Get("HERO_NAME_BOOMER_BADLAD"));
		}

		public override bool IsAvailable(Simulator sim)
		{
			return sim.IsHeroUnlocked("BOMBERMAN");
		}
	}
}
