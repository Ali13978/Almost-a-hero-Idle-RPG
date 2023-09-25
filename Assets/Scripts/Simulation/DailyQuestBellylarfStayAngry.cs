using System;
using Ui;

namespace Simulation
{
	public class DailyQuestBellylarfStayAngry : DailyQuest
	{
		public DailyQuestBellylarfStayAngry()
		{
			this.goal = 600;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 95.0)
			{
				uiState = UiState.HUB_ACHIEVEMENTS
			};
		}

		public override void OnBellylarfAnger(int count)
		{
			this.progress = GameMath.GetMinInt(this.progress + count, this.goal);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 0.75f;
		}

		public override string GetQuestString()
		{
			return string.Format(LM.Get("DAILY_QUEST_BELLYLARF_ANGER"), LM.Get("HERO_NAME_BELLYLARF"));
		}

		public override bool IsAvailable(Simulator sim)
		{
			return true;
		}
	}
}
