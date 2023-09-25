using System;
using Ui;

namespace Simulation
{
	public class DailyQuestSamShield : DailyQuest
	{
		public DailyQuestSamShield()
		{
			this.goal = 80;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 90.0)
			{
				uiState = UiState.HUB_ACHIEVEMENTS
			};
		}

		public override void OnSamShield(int count)
		{
			this.progress = GameMath.GetMinInt(this.progress + count, this.goal);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 0.8f;
		}

		public override string GetQuestString()
		{
			return string.Format(LM.Get("DAILY_QUEST_SAM_SHIELD"), LM.Get("HERO_NAME_SAM"));
		}

		public override bool IsAvailable(Simulator sim)
		{
			return sim.IsHeroUnlocked("SAM");
		}
	}
}
