using System;

namespace Simulation
{
	public abstract class MerchantItem
	{
		public MerchantItem()
		{
			this.level = -1;
		}

		public abstract string GetId();

		public abstract string GetTitleString();

		public abstract string GetDescriptionString(Simulator sim, int count);

		public virtual string GetSecondaryDescriptionString(Simulator sim)
		{
			return null;
		}

		public string GetDescriptionString(Simulator sim)
		{
			return this.GetDescriptionString(sim, 1);
		}

		public virtual void Apply(Simulator sim)
		{
			if (!this.IsUnlocked())
			{
				throw new Exception("Can not use locked merchant item.");
			}
		}

		public bool IsUnlocked()
		{
			return this.level >= 0;
		}

		public void SetLevel(int newLevel)
		{
			this.level = newLevel;
		}

		public void SetNumMaxBase(int numMaxBase)
		{
			this.numMaxBase = numMaxBase;
		}

		public int GetNumMax()
		{
			return this.numMaxBase + this.numMaxAdd;
		}

		public void SetNumMaxAdd(int numMaxAdd)
		{
			this.numMaxAdd = numMaxAdd;
		}

		public int GetLevelForLoading()
		{
			return this.level;
		}

		public int GetNumLeft()
		{
			return this.numMaxBase + this.numMaxAdd - this.numUsed;
		}

		public void SetNumUsed(int numUsed)
		{
			this.numUsed = numUsed;
		}

		public void SetNumInInventory(int numInInventory)
		{
			this.numInInventory = numInInventory;
		}

		public void SetPrice(double price)
		{
			this.price = price;
		}

		public string GetNumLeftString()
		{
			if (this.GetNumLeft() <= 0)
			{
				return LM.Get("UI_MERCHANT_ITEM_SOLDOUT");
			}
			return string.Format(LM.Get("UI_MERCHANT_ITEM_HOWMANY"), this.GetNumLeft().ToString());
		}

		public string GetNumInInventoryString()
		{
			return string.Format(LM.Get("UI_MERCHANT_ITEM_HOWMANY"), this.GetNumInInventory().ToString());
		}

		public double GetPrice()
		{
			return this.price;
		}

		public string GetPriceString()
		{
			return GameMath.GetDoubleString(this.price);
		}

		public string GetPriceString(int count)
		{
			return GameMath.GetDoubleString(this.price * (double)count);
		}

		public bool CanAfford(Currency tokens)
		{
			return tokens.CanAfford(this.price);
		}

		public bool CanAffordAndIsLeft(Currency tokens)
		{
			return this.GetNumInInventory() > 0 || (tokens.CanAfford(this.price) && this.GetNumLeft() > 0);
		}

		public bool CanAffordAndIsLeft(Currency tokens, int count)
		{
			return this.GetNumInInventory() > 0 || (tokens.CanAfford(this.price * (double)count) && this.GetNumLeft() > count - 1);
		}

		public void Buy()
		{
			if (this.GetNumInInventory() > 0)
			{
				this.numInInventory--;
			}
			else
			{
				if (this.GetNumLeft() <= 0)
				{
					throw new Exception("Cant buy merchant item cuz none left!");
				}
				this.numUsed++;
			}
		}

		public int GetNumUsed()
		{
			return this.numUsed;
		}

		public int GetNumInInventory()
		{
			return this.numInInventory;
		}

		public void AddNumInInventory(int count)
		{
			this.numInInventory += count;
		}

		public virtual bool CanEvaulate(World world)
		{
			return true;
		}

		protected int level;

		private int numMaxBase;

		private int numMaxAdd;

		private int numUsed;

		private int numInInventory;

		private double price;
	}
}
