using System;
using Simulation;

namespace stats
{
	public class BadgeSnakeEater : Badge
	{
		public override BadgeId Id
		{
			get
			{
				return BadgeId.SnakeEater;
			}
		}

		public override string Description
		{
			get
			{
				return LM.Get("UI_BADGE_DEFEAT_GOG100");
			}
		}

		public override bool CanBeObtained(Simulator simulator)
		{
			return simulator.GetWorld(GameMode.RIFT).IsModeUnlocked();
		}

		protected override bool updateEarnedState(Simulator simulator)
		{
			return simulator.GetLastUnlockedRiftChallengeIndex() >= 100;
		}
	}
}
