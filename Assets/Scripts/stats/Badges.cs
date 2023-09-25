using System;
using System.Collections.Generic;
using Simulation;

namespace stats
{
	public static class Badges
	{
		public static List<Badge> GetDisplayableBadges(Simulator simulator)
		{
			Badges.cachedDisplayableBadgesList.Clear();
			for (int i = 0; i < Badges.All.Length; i++)
			{
				if (Badges.All[i].HasBeenEarnedByPlayer(simulator) || Badges.All[i].CanBeObtained(simulator))
				{
					Badges.cachedDisplayableBadgesList.Add(Badges.All[i]);
				}
			}
			return Badges.cachedDisplayableBadgesList;
		}

		public static int GetNumBadgesToNotify(Simulator simulator)
		{
			int num = 0;
			for (int i = 0; i < Badges.All.Length; i++)
			{
				if (Badges.All[i].IsNotificationEnabled(simulator))
				{
					num++;
				}
			}
			return num;
		}

		public static Badge GetBadgeWithId(BadgeId id)
		{
			int num = 0;
			while (Badges.All[num].Id != id)
			{
				num++;
			}
			return Badges.All[num];
		}

		public static void Reset()
		{
			foreach (Badge badge in Badges.All)
			{
				badge.LoadState(false, false);
			}
		}

		public static void Unlock()
		{
			foreach (Badge badge in Badges.All)
			{
				badge.LoadState(true, true);
			}
		}

		public static readonly Badge[] All = new Badge[]
		{
			new BadgeCataclysmSurviver(),
			new BadgeWintertideParticipant(),
			new BadgeWintertadeTopOfTree(),
			new BadgeWintertideCollector(),
			new BadgeSnakeEater(),
			new BadgeOneYearAnniversaryParticipant(),
			new BadgeTwoYearsAnniversaryParticipant()
		};

		private static List<Badge> cachedDisplayableBadgesList = new List<Badge>();
	}
}
