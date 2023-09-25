using System;
using Ui;

namespace Simulation
{
	public class DailyQuestGoblinKillTreasure : DailyQuest
	{
		public DailyQuestGoblinKillTreasure()
		{
			this.goal = 80;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 80.0)
			{
				uiState = UiState.HUB_ACHIEVEMENTS
			};
		}

		public override void OnGoblinKillTreasure(int count)
		{
			this.progress = GameMath.GetMinInt(this.progress + count, this.goal);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 1f;
		}

		public override string GetQuestString()
		{
			return string.Format(LM.Get("DAILY_QUEST_GOBLIN_KILL_TREASURE"), LM.Get("HERO_NAME_GOBLIN"));
		}

		public override bool IsAvailable(Simulator sim)
		{
			return sim.IsHeroUnlocked("GOBLIN");
		}
	}
}
