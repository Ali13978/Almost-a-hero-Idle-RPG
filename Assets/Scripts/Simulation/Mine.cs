using System;

namespace Simulation
{
	public abstract class Mine
	{
		public double GetPeriod()
		{
			return this.period;
		}

		public bool IsMaxed()
		{
			return this.level >= this.CURRENCY_REWARDS.Length - 1;
		}

		public double GetReward()
		{
			if (this.level >= this.CURRENCY_REWARDS.Length)
			{
				return this.CURRENCY_REWARDS[this.CURRENCY_REWARDS.Length - 1];
			}
			return this.CURRENCY_REWARDS[this.level];
		}

		public double GetNextLevelReward()
		{
			if (this.level + 1 >= this.CURRENCY_REWARDS.Length)
			{
				return this.GetReward();
			}
			return this.CURRENCY_REWARDS[this.level + 1];
		}

		public double GetUpgradeCost()
		{
			if (this.level >= this.UPGRADE_COSTS.Length - 1)
			{
				return -1.0;
			}
			return this.UPGRADE_COSTS[this.level];
		}

		public double GetUpgradeRewardDiff()
		{
			if (this.level >= this.CURRENCY_REWARDS.Length - 1)
			{
				return 0.0;
			}
			return this.CURRENCY_REWARDS[this.level + 1] - this.CURRENCY_REWARDS[this.level];
		}

		public bool unlocked;

		public int level;

		public CurrencyType rewardCurrency;

		public double[] CURRENCY_REWARDS = new double[]
		{
			25.0,
			30.0,
			35.0,
			40.0,
			45.0,
			50.0,
			55.0,
			60.0,
			65.0,
			70.0,
			75.0,
			80.0,
			85.0,
			90.0,
			100.0,
			110.0,
			120.0,
			130.0,
			140.0,
			150.0
		};

		public double[] UPGRADE_COSTS = new double[]
		{
			5.0,
			10.0,
			15.0,
			20.0,
			25.0,
			30.0,
			35.0,
			40.0,
			45.0,
			50.0,
			55.0,
			60.0,
			65.0,
			70.0,
			75.0,
			80.0,
			85.0,
			90.0,
			95.0,
			100.0
		};

		public DateTime timeCollected;

		protected double period;

		public const double PeriodOneDay = 75600.0;
	}
}
