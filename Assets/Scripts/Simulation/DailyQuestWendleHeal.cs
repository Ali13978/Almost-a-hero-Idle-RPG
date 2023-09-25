using System;
using Ui;

namespace Simulation
{
	public class DailyQuestWendleHeal : DailyQuest
	{
		public DailyQuestWendleHeal()
		{
			this.goal = 90;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 105.0)
			{
				uiState = UiState.HUB_ACHIEVEMENTS
			};
		}

		public override void OnWendleHeal()
		{
			this.progress = GameMath.GetMinInt(this.progress + 1, this.goal);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 1.2f;
		}

		public override string GetQuestString()
		{
			return string.Format(LM.Get("DAILY_QUEST_WENDLE_HEAL"), LM.Get("HERO_NAME_WENDLE"));
		}

		public override bool IsAvailable(Simulator sim)
		{
			return sim.IsHeroUnlocked("DEREK");
		}
	}
}
