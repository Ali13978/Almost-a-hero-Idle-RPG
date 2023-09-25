using System;
using Simulation;

namespace stats
{
	public class BadgeOneYearAnniversaryParticipant : Badge
	{
		public override BadgeId Id
		{
			get
			{
				return BadgeId.OneYearAnniversaryParticipant;
			}
		}

		public override string Description
		{
			get
			{
				return LM.Get("UI_BADGE_ANNIVERSARY");
			}
		}

		public override bool CanBeObtained(Simulator simulator)
		{
			return false;
		}

		protected override bool updateEarnedState(Simulator simulator)
		{
			return simulator.completedQuestOfUpdates.Contains(QuestOfUpdateIds.Anniversary01);
		}
	}
}
