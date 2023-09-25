using System;
using System.Collections.Generic;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelServerReward : AahMonoBehaviour
	{
		public static int GetNumRewardRowsFor(PlayfabManager.RewardData reward)
		{
			int num = 0;
			if (reward.skinIds != null && reward.skinIds.Count > 0)
			{
				num++;
			}
			int num2 = 0;
			if (reward.amountCredits > 0.0)
			{
				num2++;
			}
			if (reward.amountMythstones > 0.0)
			{
				num2++;
			}
			if (reward.amountScraps > 0.0)
			{
				num2++;
			}
			if (reward.amountToken > 0.0)
			{
				num2++;
			}
			if (reward.amountCandies > 0.0)
			{
				num2++;
			}
			if (reward.amountAeons > 0.0)
			{
				num2++;
			}
			if (reward.amountTrinketBoxes > 0)
			{
				num2++;
			}
			if (num2 > 0)
			{
				num++;
			}
			if (num2 > 3)
			{
				num++;
			}
			return num;
		}

		public void Refresh(Simulator simulator, UiManager uiManager, PlayfabManager.RewardData reward)
		{
			this.text.text = ((!reward.HasReward()) ? string.Empty : reward.desc);
			int num = 0;
			if (reward.amountCredits > 0.0)
			{
				this.GetOrInstantiateCurrencyReward(num).SetCurrency(CurrencyType.GEM, GameMath.GetDoubleString(reward.amountCredits), true, GameMode.STANDARD, true);
				num++;
			}
			if (reward.amountMythstones > 0.0)
			{
				this.GetOrInstantiateCurrencyReward(num).SetCurrency(CurrencyType.MYTHSTONE, GameMath.GetDoubleString(reward.amountMythstones), true, GameMode.STANDARD, true);
				num++;
			}
			if (reward.amountScraps > 0.0)
			{
				this.GetOrInstantiateCurrencyReward(num).SetCurrency(CurrencyType.SCRAP, GameMath.GetDoubleString(reward.amountScraps), true, GameMode.STANDARD, true);
				num++;
			}
			if (reward.amountToken > 0.0)
			{
				this.GetOrInstantiateCurrencyReward(num).SetCurrency(CurrencyType.TOKEN, GameMath.GetDoubleString(reward.amountToken), true, GameMode.STANDARD, true);
				num++;
			}
			if (reward.amountAeons > 0.0)
			{
				this.GetOrInstantiateCurrencyReward(num).SetCurrency(CurrencyType.AEON, GameMath.GetDoubleString(reward.amountAeons), true, GameMode.STANDARD, true);
				num++;
			}
			if (reward.amountCandies > 0.0)
			{
				this.GetOrInstantiateCurrencyReward(num).SetCurrency(CurrencyType.CANDY, GameMath.GetDoubleString(reward.amountCandies), true, GameMode.STANDARD, true);
				num++;
			}
			if (reward.amountTrinketBoxes > 0)
			{
				MenuShowCurrency orInstantiateCurrencyReward = this.GetOrInstantiateCurrencyReward(num);
				orInstantiateCurrencyReward.SetCurrency(CurrencyType.TRINKET_BOX, GameMath.GetDoubleString((double)reward.amountTrinketBoxes), true, GameMode.STANDARD, true);
				orInstantiateCurrencyReward.SetCurrencyImageSize(65, 65);
				num++;
			}
			int i = num;
			int count = this.menuShowCurrencyList.Count;
			while (i < count)
			{
				this.menuShowCurrencyList[i].gameObject.SetActive(false);
				i++;
			}
			this.currencyRewardsParent.gameObject.SetActive(num > 0);
			this.currencyRewardsFirstRowParent.gameObject.SetActive(num > 0);
			this.currencyRewardsSecondRowParent.gameObject.SetActive(num >= 3);
			if (reward.skinIds != null && reward.skinIds.Count > 0)
			{
				this.skinRewardsParent.gameObject.SetActive(true);
				int j = 0;
				int count2 = reward.skinIds.Count;
				while (j < count2)
				{
					this.GetOrInstantiateSkinReward(j).Init(simulator, uiManager, reward.skinIds[j]);
					j++;
				}
			}
			else
			{
				this.skinRewardsParent.gameObject.SetActive(false);
			}
			int num2 = (reward.skinIds != null) ? reward.skinIds.Count : 0;
			int k = num2;
			int count3 = this.skins.Count;
			while (k < count3)
			{
				this.skins[k].gameObject.SetActive(false);
				k++;
			}
		}

		private MenuShowCurrency GetOrInstantiateCurrencyReward(int index)
		{
			MenuShowCurrency menuShowCurrency;
			if (this.menuShowCurrencyList.Count <= index)
			{
				menuShowCurrency = UnityEngine.Object.Instantiate<MenuShowCurrency>(this.menuShowCurrencyPrefab, (index >= 3) ? this.currencyRewardsSecondRowParent : this.currencyRewardsFirstRowParent);
				this.menuShowCurrencyList.Add(menuShowCurrency);
			}
			else
			{
				menuShowCurrency = this.menuShowCurrencyList[index];
				menuShowCurrency.gameObject.SetActive(true);
			}
			return menuShowCurrency;
		}

		private ServerRewardSkin GetOrInstantiateSkinReward(int index)
		{
			ServerRewardSkin serverRewardSkin;
			if (this.skins.Count <= index)
			{
				serverRewardSkin = UnityEngine.Object.Instantiate<ServerRewardSkin>(this.skinPrefab, this.skinRewardsParent);
				this.skins.Add(serverRewardSkin);
			}
			else
			{
				serverRewardSkin = this.skins[index];
				if (!serverRewardSkin.gameObject.activeSelf)
				{
					serverRewardSkin.gameObject.SetActive(true);
				}
			}
			return serverRewardSkin;
		}

		[NonSerialized]
		public float timer;

		[SerializeField]
		private Text text;

		[SerializeField]
		private MenuShowCurrency menuShowCurrencyPrefab;

		[SerializeField]
		private ServerRewardSkin skinPrefab;

		[SerializeField]
		private RectTransform currencyRewardsParent;

		[SerializeField]
		private RectTransform currencyRewardsFirstRowParent;

		[SerializeField]
		private RectTransform currencyRewardsSecondRowParent;

		[SerializeField]
		private RectTransform skinRewardsParent;

		public RectTransform popupRect;

		private const int MAX_CURRENCY_REWARDS_PER_ROW = 3;

		private List<MenuShowCurrency> menuShowCurrencyList = new List<MenuShowCurrency>();

		private List<ServerRewardSkin> skins = new List<ServerRewardSkin>();
	}
}
