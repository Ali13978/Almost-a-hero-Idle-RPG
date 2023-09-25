using System;
using Static;

namespace Simulation
{
	public class Currency
	{
		public Currency(CurrencyType type)
		{
			this.type = type;
			this.InitZero();
		}

		public void InitZero()
		{
			this.amount = 0.0;
		}

		public void Increment(double x)
		{
			this.amount += x;
		}

		public void Decrement(double x)
		{
			this.amount -= x;
			PlayerStats.OnCurrencySpent(this.type, x);
		}

		public bool CanAfford(double price)
		{
			return price <= this.amount + 0.49;
		}

		public double GetAmount()
		{
			return this.amount;
		}

		public void SetAmountForLoading(double amount)
		{
			this.amount = amount;
		}

		public string GetString()
		{
			return GameMath.GetDoubleString(this.amount);
		}

		private CurrencyType type;

		private double amount;
	}
}
