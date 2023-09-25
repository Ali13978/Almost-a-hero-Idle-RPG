using System;
using System.Collections.Generic;
using Simulation;

namespace stats
{
	public class BadgeWintertideCollector : Badge
	{
		public override BadgeId Id
		{
			get
			{
				return BadgeId.WintertideCollector;
			}
		}

		public override string Description
		{
			get
			{
				return LM.Get("UI_BADGE_WINTERTIDE_COMPLETE");
			}
		}

		public override bool CanBeObtained(Simulator simulator)
		{
			return simulator.IsChristmasTreeEnabled();
		}

		protected override bool updateEarnedState(Simulator simulator)
		{
			if (simulator.christmasOfferBundle == null)
			{
				return false;
			}
			for (int i = simulator.christmasOfferBundle.tree.Count - 1; i >= 0; i--)
			{
				List<CalendarTreeOfferNode> list = simulator.christmasOfferBundle.tree[i];
				for (int j = list.Count - 1; j >= 0; j--)
				{
					if (list[j].offer.purchasesLeft > 0)
					{
						return false;
					}
				}
			}
			return true;
		}
	}
}
