using System;
using System.Collections.Generic;

namespace Simulation
{
	public class CharmComparer
	{
		public CharmComparer()
		{
			this.generalSorter = new CharmComparer.General
			{
				levelComparer = new CharmComparer.Level(),
				typeComparer = new CharmComparer.Type(),
				levelupStatusComparer = new CharmComparer.LevelupStatus()
			};
		}

		public static CharmSortType sortType;

		public static bool isDescending;

		public CharmComparer.General generalSorter;

		public class Level : IComparer<CharmEffectData>
		{
			public int Compare(CharmEffectData x, CharmEffectData y)
			{
				return (!CharmComparer.isDescending) ? x.level.CompareTo(y.level) : (-x.level.CompareTo(y.level));
			}
		}

		public class Type : IComparer<CharmEffectData>
		{
			public int Compare(CharmEffectData x, CharmEffectData y)
			{
				int charmType = (int)x.BaseData.charmType;
				int charmType2 = (int)y.BaseData.charmType;
				return (!CharmComparer.isDescending) ? charmType.CompareTo(charmType2) : (-charmType.CompareTo(charmType2));
			}
		}

		public class LevelupStatus : IComparer<CharmEffectData>
		{
			public int Compare(CharmEffectData x, CharmEffectData y)
			{
				float progress = x.GetProgress();
				float progress2 = y.GetProgress();
				return (!CharmComparer.isDescending) ? progress.CompareTo(progress2) : (-progress.CompareTo(progress2));
			}
		}

		public class General : IComparer<CharmEffectData>
		{
			public int Compare(CharmEffectData x, CharmEffectData y)
			{
				int num = 0;
				if (x.IsLocked() && !y.IsLocked())
				{
					num = 1;
				}
				else if (!x.IsLocked() && y.IsLocked())
				{
					num = -1;
				}
				else if (x.IsLocked() && y.IsLocked())
				{
					return 0;
				}
				if (num != 0 || CharmComparer.sortType == CharmSortType.Default)
				{
					return num;
				}
				if (CharmComparer.sortType == CharmSortType.Level)
				{
					return this.levelComparer.Compare(x, y);
				}
				if (CharmComparer.sortType == CharmSortType.Type)
				{
					return this.typeComparer.Compare(x, y);
				}
				if (CharmComparer.sortType == CharmSortType.LevelupStatus)
				{
					return this.levelupStatusComparer.Compare(x, y);
				}
				throw new Exception("Un defined charm sort option: " + CharmComparer.sortType.ToString());
			}

			public IComparer<CharmEffectData> levelComparer;

			public IComparer<CharmEffectData> typeComparer;

			public IComparer<CharmEffectData> levelupStatusComparer;
		}
	}
}
