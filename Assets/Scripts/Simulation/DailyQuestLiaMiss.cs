using System;
using Ui;

namespace Simulation
{
	public class DailyQuestLiaMiss : DailyQuest
	{
		public DailyQuestLiaMiss()
		{
			this.goal = 750;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 120.0)
			{
				uiState = UiState.HUB_ACHIEVEMENTS
			};
		}

		public override void OnLiaMiss(int count)
		{
			this.progress = GameMath.GetMinInt(this.goal, this.progress + count);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 0.75f;
		}

		public override string GetQuestString()
		{
			return string.Format(LM.Get("DAILY_QUEST_LIA_MISS"), LM.Get("HERO_NAME_LIA"));
		}

		public override bool IsAvailable(Simulator sim)
		{
			return sim.IsHeroUnlocked("BLIND_ARCHER");
		}
	}
}
