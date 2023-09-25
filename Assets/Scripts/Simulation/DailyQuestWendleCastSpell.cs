using System;
using Ui;

namespace Simulation
{
	public class DailyQuestWendleCastSpell : DailyQuest
	{
		public DailyQuestWendleCastSpell()
		{
			this.goal = 100;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 100.0)
			{
				uiState = UiState.HUB_ACHIEVEMENTS
			};
		}

		public override void OnWendleCast()
		{
			this.progress = GameMath.GetMinInt(this.progress + 1, this.goal);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 0.8f;
		}

		public override string GetQuestString()
		{
			return string.Format(LM.Get("DAILY_QUEST_WENDLE_CAST_SPELL"), LM.Get("HERO_NAME_WENDLE"));
		}

		public override bool IsAvailable(Simulator sim)
		{
			return sim.IsHeroUnlocked("DEREK");
		}
	}
}
