using System;
using Simulation;

namespace stats
{
	public class BadgeCataclysmSurviver : Badge
	{
		public override BadgeId Id
		{
			get
			{
				return BadgeId.CataclysmSurviver;
			}
		}

		public override string Description
		{
			get
			{
				return LM.Get("UI_BADGE_CATACLYSM");
			}
		}

		public override bool CanBeObtained(Simulator simulator)
		{
			return false;
		}

		protected override bool updateEarnedState(Simulator simulator)
		{
			return simulator.isCataclysmSurviver;
		}
	}
}
