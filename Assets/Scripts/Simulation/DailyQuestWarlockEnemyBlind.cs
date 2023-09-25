using System;
using Ui;

namespace Simulation
{
	public class DailyQuestWarlockEnemyBlind : DailyQuest
	{
		public DailyQuestWarlockEnemyBlind()
		{
			this.goal = 200;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 100.0)
			{
				uiState = UiState.HUB_ACHIEVEMENTS
			};
		}

		public override void OnWarlockBlindEnemy(int count)
		{
			this.progress = GameMath.GetMinInt(this.progress + count, this.goal);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 1f;
		}

		public override string GetQuestString()
		{
			return string.Format(LM.Get("DAILY_QUEST_WARLOCK_BLIND_ENEMY"), LM.Get("HERO_NAME_WARLOCK"));
		}

		public override bool IsAvailable(Simulator sim)
		{
			return sim.IsHeroUnlocked("WARLOCK");
		}
	}
}
