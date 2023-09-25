using System;
using Ui;

namespace Simulation
{
	public class DailyQuestGoblinSummonDragons : DailyQuest
	{
		public DailyQuestGoblinSummonDragons()
		{
			this.goal = 42;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 100.0)
			{
				uiState = UiState.HUB_ACHIEVEMENTS
			};
		}

		public override void OnGoblinSummonDragon(int count)
		{
			this.progress = GameMath.GetMinInt(this.progress + count, this.goal);
		}

		public override void OnGoblinMiss(int count)
		{
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 1f;
		}

		public override string GetQuestString()
		{
			return string.Format(LM.Get("DAILY_QUEST_GOBLIN_SUMMON_DRAGON"), LM.Get("HERO_NAME_GOBLIN"));
		}

		public override bool IsAvailable(Simulator sim)
		{
			return sim.IsHeroUnlocked("GOBLIN");
		}
	}
}
