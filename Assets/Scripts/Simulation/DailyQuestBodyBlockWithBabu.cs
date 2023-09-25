using System;
using Ui;

namespace Simulation
{
	public class DailyQuestBodyBlockWithBabu : DailyQuest
	{
		public DailyQuestBodyBlockWithBabu()
		{
			this.goal = 300;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 100.0)
			{
				uiState = UiState.HUB_ACHIEVEMENTS
			};
		}

		public override void OnBabuGetHit(int count)
		{
			this.progress = GameMath.GetMinInt(this.progress + count, this.goal);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 1f;
		}

		public override string GetQuestString()
		{
			return string.Format(LM.Get("DAILY_QUEST_BODY_BLOCK_W_BABU"), LM.Get("HERO_NAME_BABU"));
		}

		public override bool IsAvailable(Simulator sim)
		{
			return sim.IsHeroUnlocked("BABU");
		}
	}
}
