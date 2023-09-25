using System;
using Ui;

namespace Simulation
{
	public class DailyQuestTamHitMarkedTargets : DailyQuest
	{
		public DailyQuestTamHitMarkedTargets()
		{
			this.goal = 60;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 90.0)
			{
				uiState = UiState.HUB_ACHIEVEMENTS
			};
		}

		public override void OnTamHitMarkedTargets(int count)
		{
			this.progress = GameMath.GetMinInt(this.progress + count, this.goal);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 1f;
		}

		public override string GetQuestString()
		{
			return string.Format(LM.Get("DAILY_QUEST_TAM_HIT_MARKED_TARGETS"), LM.Get("HERO_NAME_TAM"));
		}

		public override bool IsAvailable(Simulator sim)
		{
			return sim.IsHeroUnlocked("TAM");
		}
	}
}
