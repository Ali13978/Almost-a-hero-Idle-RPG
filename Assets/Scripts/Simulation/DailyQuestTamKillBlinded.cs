using System;
using Ui;

namespace Simulation
{
	public class DailyQuestTamKillBlinded : DailyQuest
	{
		public DailyQuestTamKillBlinded()
		{
			this.goal = 30;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 90.0)
			{
				uiState = UiState.HUB_ACHIEVEMENTS
			};
		}

		public override void OnTamKillBlinded(int count)
		{
			this.progress = GameMath.GetMinInt(this.progress + count, this.goal);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 1f;
		}

		public override string GetQuestString()
		{
			return string.Format(LM.Get("DAILY_QUEST_TAM_KILL_BLINDED"), LM.Get("HERO_NAME_TAM"));
		}

		public override bool IsAvailable(Simulator sim)
		{
			return sim.IsHeroUnlocked("TAM");
		}
	}
}
