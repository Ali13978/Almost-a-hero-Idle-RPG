using System;
using Ui;

namespace Simulation
{
	public class DailyQuestVTreasureChestKill : DailyQuest
	{
		public DailyQuestVTreasureChestKill()
		{
			this.goal = 50;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 75.0)
			{
				uiState = UiState.HUB_ACHIEVEMENTS
			};
		}

		public override void OnVTreasureChestKill(int count)
		{
			this.progress = GameMath.GetMinInt(this.progress + count, this.goal);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 1f;
		}

		public override string GetQuestString()
		{
			return string.Format(LM.Get("DAILY_QUEST_V_TREASURE_CHEST_KILL"), LM.Get("HERO_NAME_V"));
		}

		public override bool IsAvailable(Simulator sim)
		{
			return sim.IsHeroUnlocked("SHEELA");
		}
	}
}
