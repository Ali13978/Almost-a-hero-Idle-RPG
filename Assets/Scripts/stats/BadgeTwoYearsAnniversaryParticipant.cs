using System;
using Simulation;

namespace stats
{
	public class BadgeTwoYearsAnniversaryParticipant : Badge
	{
		public override BadgeId Id
		{
			get
			{
				return BadgeId.TwoYearsAnniversaryParticipant;
			}
		}

		public override string Description
		{
			get
			{
				return LM.Get("UI_BADGE_ANNIVERSARY_2");
			}
		}

		public override bool CanBeObtained(Simulator simulator)
		{
			return simulator.IsSecondAnniversaryEventEnabled() && TutorialManager.prestige == TutorialManager.Prestige.FIN;
		}

		protected override bool updateEarnedState(Simulator simulator)
		{
			return simulator.prestigedDuringSecondAnniversaryEvent;
		}
	}
}
