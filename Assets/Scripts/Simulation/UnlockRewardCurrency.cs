using System;
using Ui;
using UnityEngine;

namespace Simulation
{
	public class UnlockRewardCurrency : UnlockReward
	{
		public UnlockRewardCurrency(CurrencyType currencyType, double amount)
		{
			this.currencyType = currencyType;
			this.amount = amount;
		}

		public override UnlockReward.RewardCategory rewardCategory
		{
			get
			{
				return UnlockReward.RewardCategory.CURRENCY;
			}
		}

		public double GetAmount()
		{
			return this.amount;
		}

		public override void Give(Simulator sim, World world)
		{
			this.GiveRewardBasedOnUiState(sim, world, this.amount);
		}

		public void Give(Simulator sim, World world, double amountModifier)
		{
			this.GiveRewardBasedOnUiState(sim, world, this.amount * amountModifier);
		}

		public void GiveSilently(Simulator sim)
		{
			sim.GetCurrency(this.currencyType).Increment(this.amount);
		}

		public void RainCurrency(World world)
		{
			this.RainCurrency(world, this.amount);
		}

		private void GiveRewardBasedOnUiState(Simulator sim, World world, double amountToGive)
		{
			if (this.uiState == UiState.NONE)
			{
				this.RainCurrency(world, amountToGive);
			}
			else
			{
				world.RainCurrencyOnUi(UiState.HUB_ACHIEVEMENTS, this.currencyType, amountToGive, this.dropPosition, 30, 0f, 0f, 1f, null, 0f);
			}
		}

		private void RainCurrency(World world, double amountReward)
		{
			int num = 30;
			if (amountReward < (double)num)
			{
				num = (int)amountReward;
			}
			double num2 = amountReward / (double)num;
			for (int i = 0; i < num; i++)
			{
				Vector3 vector = new Vector3(GameMath.GetRandomFloat(-0.4f, 0.4f, GameMath.RandType.NoSeed), GameMath.GetRandomFloat(-0.35f, 0.25f, GameMath.RandType.NoSeed), 0f);
				Vector3 startPos = vector;
				Vector3 velStart = new Vector3(GameMath.GetRandomFloat(-0.35f, 0.35f, GameMath.RandType.NoSeed), 0f, 0f);
				startPos.y = (float)i * 0.2f + 1.15f;
				DropCurrency dropCurrency = new DropCurrency(this.currencyType, num2, world, false);
				dropCurrency.InitVel(0f, startPos, vector.y, velStart);
				world.drops.Add(dropCurrency);
			}
		}

		public override string GetRewardString()
		{
			switch (this.currencyType)
			{
			case CurrencyType.GOLD:
				return string.Format(LM.Get("UNLOCK_REWARD_GOLD"), GameMath.GetDoubleString(this.amount));
			case CurrencyType.SCRAP:
				return string.Format(LM.Get("UNLOCK_REWARD_SCRAPS"), GameMath.GetDoubleString(this.amount));
			case CurrencyType.MYTHSTONE:
				return string.Format(LM.Get("UNLOCK_REWARD_MYTHSTONES"), GameMath.GetDoubleString(this.amount));
			case CurrencyType.GEM:
				return string.Format(LM.Get("UNLOCK_REWARD_GEMS"), GameMath.GetDoubleString(this.amount));
			case CurrencyType.TOKEN:
				return string.Format(LM.Get("UNLOCK_REWARD_TOKENS"), GameMath.GetDoubleString(this.amount));
			case CurrencyType.AEON:
				return string.Format(LM.Get("UNLOCK_REWARD_AEONS"), GameMath.GetDoubleString(this.amount));
			default:
				throw new NotImplementedException();
			}
		}

		public override string GetRewardedString()
		{
			switch (this.currencyType)
			{
			case CurrencyType.GOLD:
				return string.Format(LM.Get("UNLOCK_REWARDED_GOLD"), GameMath.GetDoubleString(this.amount));
			case CurrencyType.SCRAP:
				return string.Format(LM.Get("UNLOCK_REWARDED_SCRAPS"), GameMath.GetDoubleString(this.amount));
			case CurrencyType.MYTHSTONE:
				return string.Format(LM.Get("UNLOCK_REWARDED_MYTHSTONES"), GameMath.GetDoubleString(this.amount));
			case CurrencyType.GEM:
				return string.Format(LM.Get("UNLOCK_REWARDED_GEMS"), GameMath.GetDoubleString(this.amount));
			case CurrencyType.TOKEN:
				return string.Format(LM.Get("UNLOCK_REWARDED_TOKENS"), GameMath.GetDoubleString(this.amount));
			case CurrencyType.AEON:
				return string.Format(LM.Get("UNLOCK_REWARDED_AEONS"), GameMath.GetDoubleString(this.amount));
			default:
				throw new NotImplementedException();
			}
		}

		public CurrencyType currencyType;

		protected double amount;

		public UiState uiState;

		public DropPosition dropPosition;
	}
}
