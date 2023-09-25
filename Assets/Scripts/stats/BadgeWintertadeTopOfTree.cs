using System;
using System.Collections.Generic;
using Simulation;

namespace stats
{
	public class BadgeWintertadeTopOfTree : Badge
	{
		public override BadgeId Id
		{
			get
			{
				return BadgeId.WintertadeTopOfTree;
			}
		}

		public override string Description
		{
			get
			{
				return LM.Get("UI_BADGE_WINTERTIDE_TOP");
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
			List<CalendarTreeOfferNode> lastItem = simulator.christmasOfferBundle.tree.GetLastItem<List<CalendarTreeOfferNode>>();
			CalendarTreeOfferNode calendarTreeOfferNode = lastItem[0];
			return calendarTreeOfferNode.offer.purchasesLeft == 0;
		}
	}
}
