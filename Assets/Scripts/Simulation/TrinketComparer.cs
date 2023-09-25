using System;
using System.Collections.Generic;

namespace Simulation
{
	public class TrinketComparer
	{
		public TrinketComparer(Simulator sim)
		{
			this.equippedComparer = new TrinketComparer.Default
			{
				sim = sim
			};
			this.numberOfEffectsComparer = new TrinketComparer.NumberOfEffects();
			this.levelComparer = new TrinketComparer.Level();
			this.colorComparer = new TrinketComparer.Color();
			this.globalComparer = new TrinketComparer.GlobalComparer
			{
				defaultComparer = this.equippedComparer,
				numberOfEffectsComparer = this.numberOfEffectsComparer,
				levelComparer = this.levelComparer,
				colorComparer = this.colorComparer,
				sim = sim
			};
		}

		public IComparer<Trinket> GetComparer(TrinketSortType sortType = TrinketSortType.Default, bool isDescending = true)
		{
			this.globalComparer.sortType = sortType;
			this.globalComparer.isDescending = isDescending;
			return this.globalComparer;
		}

		public TrinketComparer.Default equippedComparer;

		public TrinketComparer.NumberOfEffects numberOfEffectsComparer;

		public TrinketComparer.Level levelComparer;

		public TrinketComparer.Color colorComparer;

		public TrinketComparer.GlobalComparer globalComparer;

		public class Default : IComparer<Trinket>
		{
			public int Compare(Trinket x, Trinket y)
			{
				int num = this.sim.allTrinkets.IndexOf(x);
				int value = this.sim.allTrinkets.IndexOf(y);
				int num2 = -1;
				int item;
				if (this.sim.trinketsPinnedHashSet.TryGetValue(x, out item))
				{
					num2 = this.sim.trinketsPinned.IndexOf(item);
				}
				int value2 = -1;
				int item2;
				if (this.sim.trinketsPinnedHashSet.TryGetValue(y, out item2))
				{
					value2 = this.sim.trinketsPinned.IndexOf(item2);
				}
				int num3 = (!this.isDescending) ? num2.CompareTo(value2) : (-num2.CompareTo(value2));
				if (num3 == 0)
				{
					num3 = ((!this.isDescending) ? num.CompareTo(value) : (-num.CompareTo(value)));
				}
				return num3;
			}

			public bool isDescending;

			public Simulator sim;
		}

		public class NumberOfEffects : IComparer<Trinket>
		{
			public int Compare(Trinket x, Trinket y)
			{
				double num = (double)x.effects.Count;
				double value = (double)y.effects.Count;
				return (!this.isDescending) ? num.CompareTo(value) : (-num.CompareTo(value));
			}

			public bool isDescending;
		}

		public class Level : IComparer<Trinket>
		{
			public int Compare(Trinket x, Trinket y)
			{
				int totalLevel = x.GetTotalLevel();
				int totalLevel2 = y.GetTotalLevel();
				return (!this.isDescending) ? totalLevel.CompareTo(totalLevel2) : (-totalLevel.CompareTo(totalLevel2));
			}

			public bool isDescending;
		}

		public class Color : IComparer<Trinket>
		{
			public int Compare(Trinket x, Trinket y)
			{
				int bodyColorIndex = x.bodyColorIndex;
				int bodyColorIndex2 = y.bodyColorIndex;
				return (!this.isDescending) ? bodyColorIndex.CompareTo(bodyColorIndex2) : (-bodyColorIndex.CompareTo(bodyColorIndex2));
			}

			public bool isDescending;
		}

		public class GlobalComparer : IComparer<Trinket>
		{
			public int Compare(Trinket x, Trinket y)
			{
				this.numberOfEffectsComparer.isDescending = this.isDescending;
				this.levelComparer.isDescending = this.isDescending;
				this.colorComparer.isDescending = this.isDescending;
				int num = (this.sim.IsTrinketPinned(x) < 0) ? -1 : 0;
				int value = (this.sim.IsTrinketPinned(y) < 0) ? -1 : 0;
				int num2 = -num.CompareTo(value);
				if (num2 != 0)
				{
					return num2;
				}
				if (this.sortType == TrinketSortType.Default)
				{
					return this.defaultComparer.Compare(x, y);
				}
				if (this.sortType == TrinketSortType.NumberOfEffects)
				{
					int num3 = this.numberOfEffectsComparer.Compare(x, y);
					if (num3 == 0)
					{
						return this.defaultComparer.Compare(x, y);
					}
					return num3;
				}
				else if (this.sortType == TrinketSortType.Level)
				{
					int num4 = this.levelComparer.Compare(x, y);
					if (num4 == 0)
					{
						return this.defaultComparer.Compare(x, y);
					}
					return num4;
				}
				else
				{
					if (this.sortType != TrinketSortType.Color)
					{
						throw new Exception();
					}
					int num5 = this.colorComparer.Compare(x, y);
					if (num5 == 0)
					{
						return this.defaultComparer.Compare(x, y);
					}
					return num5;
				}
			}

			public TrinketComparer.Default defaultComparer;

			public TrinketComparer.NumberOfEffects numberOfEffectsComparer;

			public TrinketComparer.Level levelComparer;

			public TrinketComparer.Color colorComparer;

			public Simulator sim;

			public bool isDescending;

			public TrinketSortType sortType;
		}
	}
}
