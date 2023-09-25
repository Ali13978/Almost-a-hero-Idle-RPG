using System;
using Ui;

namespace Simulation
{
	public class DailyQuestTriggerRonImpulsiveSkill : DailyQuest
	{
		public DailyQuestTriggerRonImpulsiveSkill()
		{
			this.goal = 80;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 90.0)
			{
				uiState = UiState.HUB_ACHIEVEMENTS
			};
		}

		public override void OnRonImpulsiveSkillTriggered()
		{
			this.progress = GameMath.GetMinInt(this.goal, this.progress + 1);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 1f;
		}

		public override string GetQuestString()
		{
			return string.Format(LM.Get("DAILY_QUEST_TRIGGER_RON_IMPULSIVE_SKILL"), LM.Get("HERO_NAME_RON"));
		}

		public override bool IsAvailable(Simulator sim)
		{
			return sim.IsHeroUnlocked("DRUID");
		}
	}
}
