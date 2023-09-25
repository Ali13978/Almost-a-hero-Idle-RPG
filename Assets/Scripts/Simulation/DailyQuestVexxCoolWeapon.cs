using System;
using Ui;

namespace Simulation
{
	public class DailyQuestVexxCoolWeapon : DailyQuest
	{
		public DailyQuestVexxCoolWeapon()
		{
			this.goal = 60;
			this.reward = new UnlockRewardCurrency(CurrencyType.AEON, 90.0)
			{
				uiState = UiState.HUB_ACHIEVEMENTS
			};
		}

		public override void OnVexxCool(int count)
		{
			this.progress = GameMath.GetMinInt(this.progress + count, this.goal);
		}

		public override float GetChanceWeight(Simulator sim)
		{
			return 0.65f;
		}

		public override string GetQuestString()
		{
			return string.Format(LM.Get("DAILY_QUEST_VEXX_COOL"), LM.Get("HERO_NAME_VEXX"));
		}

		public override bool IsAvailable(Simulator sim)
		{
			return sim.IsHeroUnlocked("IDA");
		}
	}
}
