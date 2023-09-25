using System;
using System.Collections.Generic;
using Simulation;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Ui
{
	public class ShopLootpackSelect : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.InitStrings();
		}

		public override void AahUpdate(float dt)
		{
			this.timer += dt;
		}

		public void InitStrings()
		{
			this.buttonOpenPack.textDown.text = LM.Get("UI_MERCHANT_SELECT_BUY");
			this.candyNotification.text = LM.Get("CANDY_TREAT_BUY_POPUP_DESC");
		}

		public void SetPack(ShopPack shopPack, Simulator simulator)
		{
			if (shopPack is ShopPackStarter || shopPack is ShopPackRune || shopPack is ShopPackLootpackFree || shopPack is ShopPackLootpackRare || shopPack is ShopPackLootpackEpic)
			{
				this.spineChest.gameObject.SetActive(true);
				this.spineChest.SetSkin(shopPack, true);
				this.twoBagsParent.SetActive(false);
				this.threeBagsParent.SetActive(false);
				if (shopPack is ShopPackRune)
				{
					this.candyNotification.enabled = true;
					this.candyNotification.rectTransform.SetAnchorPosY(-130f);
					this.candyNotification.text = LM.Get("RUNE_PACK_NOTIFICATION");
				}
				else
				{
					this.candyNotification.enabled = false;
				}
			}
			else
			{
				this.spineChest.gameObject.SetActive(false);
				this.currencies.Clear();
				this.twoBagsParent.SetActive(true);
				this.candyNotification.enabled = (shopPack.candies > 0.0);
				if (this.candyNotification.enabled)
				{
					this.candyNotification.text = LM.Get("CANDY_TREAT_BUY_POPUP_DESC");
					this.candyNotification.rectTransform.SetAnchorPosY(-178f);
				}
				if (shopPack.credits > 0.0)
				{
					this.currencies.Add(CurrencyType.GEM);
				}
				if (shopPack.candies > 0.0)
				{
					this.currencies.Add(CurrencyType.CANDY);
				}
				if (shopPack.tokensMin > 0.0)
				{
					this.currencies.Add(CurrencyType.TOKEN);
				}
				if (shopPack.scrapsMin > 0.0)
				{
					this.currencies.Add(CurrencyType.SCRAP);
				}
				switch (this.currencies.Count)
				{
				case 1:
					this.twoBagsParent.SetActive(true);
					this.threeBagsParent.SetActive(false);
					this.twoBagsLeft.SetCurrency(this.currencies[0]);
					this.twoBagsRight.SetCurrency(this.currencies[0]);
					break;
				case 2:
					this.twoBagsParent.SetActive(true);
					this.threeBagsParent.SetActive(false);
					this.twoBagsLeft.SetCurrency(this.currencies[0]);
					this.twoBagsRight.SetCurrency(this.currencies[1]);
					break;
				case 3:
					this.twoBagsParent.SetActive(false);
					this.threeBagsParent.SetActive(true);
					this.threeBagsLeft.SetCurrency(this.currencies[0]);
					this.threeBagsCenter.SetCurrency(this.currencies[1]);
					this.threeBagsRight.SetCurrency(this.currencies[2]);
					break;
				default:
					throw new NotImplementedException();
				}
			}
			this.textBanner.text = shopPack.GetName();
			this.targetTimer = ((!shopPack.spamProtection) ? 0f : 0.5f);
			int num = 0;
			if (!shopPack.isIAP || shopPack.credits > 0.0)
			{
				MenuShowCurrency menuShowCurrency = this.menuShowCurrencyList[num++];
				menuShowCurrency.gameObject.SetActive(true);
				menuShowCurrency.SetCurrency(CurrencyType.GEM, simulator.GetCredits().GetString(), true, GameMode.STANDARD, true);
			}
			if (shopPack.scrapsMin > 0.0)
			{
				MenuShowCurrency menuShowCurrency2 = this.menuShowCurrencyList[num++];
				menuShowCurrency2.gameObject.SetActive(true);
				menuShowCurrency2.SetCurrency(CurrencyType.SCRAP, simulator.GetScraps().GetString(), true, GameMode.STANDARD, true);
			}
			if (shopPack.candies > 0.0)
			{
				MenuShowCurrency menuShowCurrency3 = this.menuShowCurrencyList[num++];
				menuShowCurrency3.gameObject.SetActive(true);
				menuShowCurrency3.SetCurrency(CurrencyType.CANDY, simulator.GetCandies().GetString(), true, GameMode.STANDARD, true);
			}
			if (shopPack.tokensMin > 0.0)
			{
				MenuShowCurrency menuShowCurrency4 = this.menuShowCurrencyList[num++];
				menuShowCurrency4.gameObject.SetActive(true);
				menuShowCurrency4.SetCurrency(CurrencyType.TOKEN, simulator.GetTokens().GetString(), true, GameMode.STANDARD, true);
			}
			for (int i = num; i < this.menuShowCurrencyList.Length; i++)
			{
				this.menuShowCurrencyList[i].gameObject.SetActive(false);
			}
		}

		public Sprite spriteBannerStandard;

		public Sprite spriteBannerRare;

		public Sprite spriteBannerEpic;

		public ChestSpine spineChest;

		public Image imageBanner;

		public Text textBanner;

		public List<PanelLootItem> panelLootItems;

		public ButtonUpgradeAnim buttonOpenPack;

		public GameButton buttonClose;

		public GameButton buttonCloseCross;

		public float timer;

		public float targetTimer;

		public MenuShowCurrency[] menuShowCurrencyList;

		public GameObject twoBagsParent;

		public GameObject threeBagsParent;

		[FormerlySerializedAs("bagLeft")]
		public ShopLootpackSelect.CurrencyBag twoBagsLeft;

		[FormerlySerializedAs("bagRight")]
		public ShopLootpackSelect.CurrencyBag twoBagsRight;

		public ShopLootpackSelect.CurrencyBag threeBagsLeft;

		public ShopLootpackSelect.CurrencyBag threeBagsCenter;

		public ShopLootpackSelect.CurrencyBag threeBagsRight;

		public Text candyNotification;

		public RectTransform popupRect;

		[NonSerialized]
		public UiState previousState;

		private const float AvoidSpamWaitTime = 0.5f;

		private List<CurrencyType> currencies = new List<CurrencyType>();

		[Serializable]
		public class CurrencyBag
		{
			public void SetCurrency(CurrencyType currencyType)
			{
				this.scraps.SetActive(currencyType == CurrencyType.SCRAP);
				this.tokens.SetActive(currencyType == CurrencyType.TOKEN);
				this.gems.SetActive(currencyType == CurrencyType.GEM);
				this.candies.SetActive(currencyType == CurrencyType.CANDY);
			}

			public CurrencyType GetCurrency()
			{
				if (this.scraps.activeSelf)
				{
					return CurrencyType.SCRAP;
				}
				if (this.tokens.activeSelf)
				{
					return CurrencyType.TOKEN;
				}
				if (this.gems.activeSelf)
				{
					return CurrencyType.GEM;
				}
				if (this.candies.activeSelf)
				{
					return CurrencyType.CANDY;
				}
				throw new NotImplementedException();
			}

			public GameObject scraps;

			public GameObject tokens;

			public GameObject gems;

			public GameObject candies;
		}
	}
}
