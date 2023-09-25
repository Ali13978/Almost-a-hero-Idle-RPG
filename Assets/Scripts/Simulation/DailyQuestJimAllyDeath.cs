using System;
using Ui;

namespace Simulation
{
	public class DailyQuestJimAllyDeath : DailyQuest
	{
		public DailyQuestJimAllyDeath()
		{
			this.goal = 25;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 95.0)
			{
				uiState = UiState.HUB_ACHIEVEMENTS
			};
		}

		public override void OnJimAllyDeath()
		{
			this.progress = GameMath.GetMinInt(this.progress + 1, this.goal);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 1f;
		}

		public override string GetQuestString()
		{
			return string.Format(LM.Get("DAILY_QUEST_JIM_ALLY_DEATH"), LM.Get("HERO_NAME_HANDSUM_JIM"));
		}

		public override bool IsAvailable(Simulator sim)
		{
			return sim.IsHeroUnlocked("JIM");
		}
	}
}
