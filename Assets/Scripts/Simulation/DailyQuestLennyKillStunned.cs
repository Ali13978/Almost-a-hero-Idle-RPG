using System;
using Ui;

namespace Simulation
{
	public class DailyQuestLennyKillStunned : DailyQuest
	{
		public DailyQuestLennyKillStunned()
		{
			this.goal = 20;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 120.0)
			{
				uiState = UiState.HUB_ACHIEVEMENTS
			};
		}

		public override void OnLennyKillStunned(int count)
		{
			this.progress = GameMath.GetMinInt(this.goal, this.progress + count);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 0.6f;
		}

		public override string GetQuestString()
		{
			return string.Format(LM.Get("DAILY_QUEST_LENNY_KILL_STUNNED"), LM.Get("HERO_NAME_KIND_LENNY"));
		}

		public override bool IsAvailable(Simulator sim)
		{
			return sim.IsHeroUnlocked("KIND_LENNY");
		}
	}
}
