using System;
using Ui;

namespace Simulation
{
	public class DailyQuestUltiSkillHard : DailyQuest
	{
		public DailyQuestUltiSkillHard()
		{
			this.goal = 90;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 130.0)
			{
				uiState = UiState.HUB_ACHIEVEMENTS
			};
		}

		public override void OnUltiSkill(int count)
		{
			this.progress = GameMath.GetMinInt(this.progress + count, this.goal);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 0.5f;
		}

		public override string GetQuestString()
		{
			return LM.Get("DAILY_QUEST_ULTI_SKILL");
		}

		public override bool IsAvailable(Simulator sim)
		{
			return true;
		}
	}
}
