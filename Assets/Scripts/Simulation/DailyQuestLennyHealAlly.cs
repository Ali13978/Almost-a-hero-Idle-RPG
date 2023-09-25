using System;
using Ui;

namespace Simulation
{
	public class DailyQuestLennyHealAlly : DailyQuest
	{
		public DailyQuestLennyHealAlly()
		{
			this.goal = 50;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 90.0)
			{
				uiState = UiState.HUB_ACHIEVEMENTS
			};
		}

		public override void OnLennyHealAlly(int count)
		{
			this.progress = GameMath.GetMinInt(this.progress + count, this.goal);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 0.6f;
		}

		public override string GetQuestString()
		{
			return string.Format(LM.Get("DAILY_QUEST_LENNY_HEAL_ALLY"), LM.Get("HERO_NAME_KIND_LENNY"));
		}

		public override bool IsAvailable(Simulator sim)
		{
			return sim.IsHeroUnlocked("KIND_LENNY");
		}
	}
}
