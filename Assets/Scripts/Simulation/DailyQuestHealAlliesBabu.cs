using System;
using Ui;

namespace Simulation
{
	public class DailyQuestHealAlliesBabu : DailyQuest
	{
		public DailyQuestHealAlliesBabu()
		{
			this.goal = 80;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 95.0)
			{
				uiState = UiState.HUB_ACHIEVEMENTS
			};
		}

		public override void OnBabuHealAlly(int count)
		{
			this.progress = GameMath.GetMinInt(this.progress + count, this.goal);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 1f;
		}

		public override string GetQuestString()
		{
			return string.Format(LM.Get("DAILY_QUEST_LENNY_HEAL_ALLY"), LM.Get("HERO_NAME_BABU"));
		}

		public override bool IsAvailable(Simulator sim)
		{
			return sim.IsHeroUnlocked("BABU");
		}
	}
}
