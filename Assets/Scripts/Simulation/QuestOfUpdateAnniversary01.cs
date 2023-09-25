using System;
using Ui;

namespace Simulation
{
	public class QuestOfUpdateAnniversary01 : QuestOfUpdate
	{
		public QuestOfUpdateAnniversary01()
		{
			this.startDate = new DateTime(2018, 3, 5);
			this.endDate = this.startDate.AddDays(47.0);
			this.goal = 1.0;
			this.id = QuestOfUpdateIds.Anniversary01;
			this.reward = new UnlockRewardCurrency(CurrencyType.GEM, 250.0);
			this.reward.uiState = UiState.HUB_ACHIEVEMENTS;
		}

		public override bool IsAvailable(Simulator sim, DateTime currentTime, bool isTimeReady)
		{
			return base.IsAvailable(sim, currentTime, isTimeReady) && sim.GetMaxStageReached() >= 1;
		}

		public override void OnPrestige()
		{
			if (!this.isExpired)
			{
				this.progress += 1.0;
			}
		}
	}
}
