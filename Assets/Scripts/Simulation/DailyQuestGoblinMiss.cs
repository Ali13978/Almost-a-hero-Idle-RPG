using System;
using Ui;

namespace Simulation
{
	public class DailyQuestGoblinMiss : DailyQuest
	{
		public DailyQuestGoblinMiss()
		{
			this.goal = 10;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 90.0)
			{
				uiState = UiState.HUB_ACHIEVEMENTS
			};
		}

		public override void OnGoblinMiss(int count)
		{
			this.progress = GameMath.GetMinInt(this.progress + count, this.goal);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 1f;
		}

		public override string GetQuestString()
		{
			return string.Format(LM.Get("DAILY_QUEST_GOBLIN_MISS"), LM.Get("HERO_NAME_GOBLIN"));
		}

		public override bool IsAvailable(Simulator sim)
		{
			return sim.IsHeroUnlocked("GOBLIN");
		}
	}
}
