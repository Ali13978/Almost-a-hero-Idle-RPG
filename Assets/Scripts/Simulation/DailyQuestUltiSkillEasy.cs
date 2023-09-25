using System;
using Ui;

namespace Simulation
{
	public class DailyQuestUltiSkillEasy : DailyQuest
	{
		public DailyQuestUltiSkillEasy()
		{
			this.goal = 40;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 70.0)
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
			return 1f;
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
