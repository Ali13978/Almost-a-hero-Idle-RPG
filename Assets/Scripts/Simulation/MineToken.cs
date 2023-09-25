using System;

namespace Simulation
{
	public class MineToken : Mine
	{
		public MineToken()
		{
			this.rewardCurrency = CurrencyType.TOKEN;
			this.BONUS_FACTORS = new double[]
			{
				1.1,
				1.2,
				1.3,
				1.45,
				1.6,
				1.75,
				1.9,
				2.1,
				2.3,
				2.5,
				2.8,
				3.1,
				3.4,
				3.7,
				4.1,
				4.5,
				5.0,
				5.5,
				6.1,
				6.7,
				7.4,
				8.1,
				8.9,
				9.8,
				10.8,
				11.8,
				13.0,
				14.0,
				15.5,
				17.0,
				19.0,
				21.0,
				23.0,
				25.5,
				28.0,
				30.5,
				34.0,
				37.0,
				41.0,
				45.0,
				49.0,
				54.0,
				60.0,
				65.0,
				70.0,
				75.0,
				80.0,
				86.0,
				92.0,
				100.0
			};
			this.CURRENCY_REWARDS = new double[]
			{
				40.0,
				50.0,
				60.0,
				70.0,
				80.0,
				90.0,
				100.0,
				110.0,
				120.0,
				140.0,
				160.0,
				180.0,
				200.0,
				240.0,
				260.0,
				280.0,
				300.0,
				330.0,
				360.0,
				400.0,
				430.0,
				460.0,
				490.0,
				520.0,
				550.0,
				580.0,
				610.0,
				640.0,
				670.0,
				700.0,
				730.0,
				760.0,
				790.0,
				820.0,
				850.0,
				860.0,
				870.0,
				880.0,
				890.0,
				900.0,
				910.0,
				920.0,
				930.0,
				940.0,
				950.0,
				960.0,
				970.0,
				980.0,
				990.0,
				1000.0
			};
			this.UPGRADE_COSTS = new double[]
			{
				160.0,
				180.0,
				210.0,
				240.0,
				270.0,
				300.0,
				340.0,
				380.0,
				430.0,
				480.0,
				540.0,
				610.0,
				650.0,
				750.0,
				850.0,
				950.0,
				1100.0,
				1200.0,
				1400.0,
				1550.0,
				1750.0,
				2000.0,
				2200.0,
				2500.0,
				2800.0,
				3200.0,
				3600.0,
				4000.0,
				4500.0,
				5100.0,
				5600.0,
				6400.0,
				7200.0,
				8200.0,
				9200.0,
				10000.0,
				11500.0,
				13000.0,
				14500.0,
				16500.0,
				18500.0,
				21000.0,
				23500.0,
				26500.0,
				30000.0,
				33500.0,
				38000.0,
				42500.0,
				48000.0,
				54000.0
			};
			this.period = 75600.0;
		}

		public double GetHealthFactor()
		{
			if (this.level >= this.BONUS_FACTORS.Length)
			{
				return this.BONUS_FACTORS[this.BONUS_FACTORS.Length - 1];
			}
			return this.BONUS_FACTORS[this.level];
		}

		public double GetDamageFactor()
		{
			if (this.level >= this.BONUS_FACTORS.Length)
			{
				return this.BONUS_FACTORS[this.BONUS_FACTORS.Length - 1];
			}
			return this.BONUS_FACTORS[this.level];
		}

		public double GetDamageFactorNext()
		{
			if (this.level + 1 >= this.BONUS_FACTORS.Length)
			{
				return this.GetDamageFactor();
			}
			return this.BONUS_FACTORS[this.level + 1];
		}

		public double[] BONUS_FACTORS;
	}
}
