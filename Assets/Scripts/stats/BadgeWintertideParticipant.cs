using System;
using Simulation;

namespace stats
{
	public class BadgeWintertideParticipant : Badge
	{
		public override BadgeId Id
		{
			get
			{
				return BadgeId.WintertadeParticipant;
			}
		}

		public override string Description
		{
			get
			{
				return LM.Get("UI_BADGE_WINTERTIDE_PARTICIPATE");
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
				CalendarTreeOfferNode calendarTreeOfferNode = simulator.christmasOfferBundle.tree[i].Find((CalendarTreeOfferNode n) => n.offer.purchasesLeft == 0);
				if (calendarTreeOfferNode != null)
				{
					return true;
				}
			}
			return false;
		}
	}
}
