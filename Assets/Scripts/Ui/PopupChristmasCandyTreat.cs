using System;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PopupChristmasCandyTreat : AahMonoBehaviour
	{
		public int IapIndex { get; private set; }

		public void InitStrings()
		{
			this.header.text = LM.Get("UI_CANDIES");
			this.body.text = LM.Get("CANDY_TREAT_BUY_POPUP_DESC");
		}

		public void SetCandyTreatIap(int iapIndex)
		{
			this.IapIndex = iapIndex;
			this.buyButton.textDown.text = LM.Get("UI_BUY");
			this.buyButton.textUp.text = IapManager.productPriceStringsLocal[iapIndex];
			this.menuShowCurrency.SetCurrency(CurrencyType.CANDY, GameMath.GetDoubleString(12500.0), false, GameMode.STANDARD, true);
			this.candyTreatImage.sprite = this.candyTreatsImages[iapIndex - IapIds.CANDY_PACK_01 + 1];
			this.buyButton.gameButton.interactable = true;
			this.buyButton.gameObject.SetActive(true);
			this.lockedButton.gameObject.SetActive(false);
			this.popupContent.SetBottomDelta(176.45f);
			this.stockParent.SetActive(false);
		}

		public void SetCandyTreatVideo()
		{
			this.IapIndex = -1;
			this.menuShowCurrency.SetCurrency(CurrencyType.CANDY, GameMath.GetDoubleString(PlayfabManager.titleData.christmasAdCandiesAmount), false, GameMode.STANDARD, true);
			this.candyTreatImage.sprite = this.candyTreatsImages[0];
			this.popupContent.SetBottomDelta(91.3f);
			this.stockParent.SetActive(true);
			this.buyButton.gameButton.interactable = false;
		}

		public void SetCandyTreatFree()
		{
			this.IapIndex = -2;
			this.buyButton.textUp.text = LM.Get("UI_SHOP_CHEST_0");
			this.buyButton.textDown.text = LM.Get("UI_COLLECT");
			this.buyButton.gameButton.interactable = true;
			this.menuShowCurrency.SetCurrency(CurrencyType.CANDY, GameMath.GetDoubleString(PlayfabManager.titleData.christmasFreeCandiesAmount), false, GameMode.STANDARD, true);
			this.candyTreatImage.sprite = this.candyTreatsImages[1];
			this.popupContent.SetBottomDelta(176.45f);
			this.stockParent.SetActive(false);
		}

		public void UpdatePopup(Simulator simulator)
		{
			this.candies.SetCurrency(CurrencyType.CANDY, simulator.GetCandies().GetString(), true, GameMode.STANDARD, true);
			if (this.IapIndex < 0)
			{
				if (this.IapIndex == -1)
				{
					this.UpdateVideoTreat(simulator);
				}
				else if (this.IapIndex == -2)
				{
					this.UpdateFreeTreat(simulator);
				}
			}
		}

		private void UpdateVideoTreat(Simulator simulator)
		{
			if (RewardedAdManager.inst != null && TrustedTime.IsReady())
			{
				int numCappedVideo = RewardedAdManager.inst.GetNumCappedVideo(simulator.GetLastCappedCurrencyWatchedTime(CurrencyType.CANDY), CurrencyType.CANDY, simulator.christmasTreatVideosWatchedSinceLastReset);
				this.stockLabel.text = string.Format(LM.Get("UI_MERCHANT_ITEM_HOWMANY"), numCappedVideo);
				if (RewardedAdManager.inst.IsRewardedCappedVideoAvailable(simulator.GetLastCappedCurrencyWatchedTime(CurrencyType.CANDY), CurrencyType.CANDY, simulator.christmasTreatVideosWatchedSinceLastReset))
				{
					this.lockedButton.gameObject.SetActive(false);
					this.buyButton.gameObject.SetActive(true);
					this.buyButton.gameButton.interactable = true;
					this.buyButton.textDown.text = LM.Get("UI_SHOP_CHEST_0");
					this.buyButton.textUp.text = LM.Get("UI_AD_WATCH");
				}
				else if (RewardedAdManager.inst.IsRewardedVideoAvailable())
				{
					this.lockedButton.gameObject.SetActive(true);
					this.buyButton.gameObject.SetActive(false);
					this.lockedButton.text.text = GameMath.GetTimeDatailedShortenedString(simulator.lastCandyAmountCapDailyReset.AddSeconds(36000.0) - TrustedTime.Get());
				}
				else
				{
					this.lockedButton.gameObject.SetActive(false);
					this.buyButton.gameObject.SetActive(true);
					this.buyButton.gameButton.interactable = false;
					this.buyButton.textDown.text = LM.Get("UI_AD_WATCH");
					this.buyButton.textDown.text = LM.Get("UI_SHOP_AD_LATER");
				}
			}
			else
			{
				this.stockLabel.text = string.Format(LM.Get("UI_MERCHANT_ITEM_HOWMANY"), 0);
				this.buyButton.gameObject.SetActive(true);
				this.lockedButton.gameObject.SetActive(false);
				this.buyButton.textUp.text = LM.Get("UI_AD_WATCH");
				this.buyButton.textDown.text = LM.Get("UI_SHOP_AD_LATER");
			}
		}

		private void UpdateFreeTreat(Simulator simulator)
		{
			if (TrustedTime.IsReady())
			{
				if (simulator.lastFreeCandyTreatClaimedDate < simulator.lastCandyAmountCapDailyReset)
				{
					this.buyButton.gameObject.SetActive(true);
					this.lockedButton.gameObject.SetActive(false);
				}
				else
				{
					this.buyButton.gameObject.SetActive(false);
					this.lockedButton.gameObject.SetActive(true);
					TimeSpan dailtyCapResetTimer = simulator.GetDailtyCapResetTimer();
					this.lockedButton.text.text = GameMath.GetTimeDatailedShortenedString(dailtyCapResetTimer);
				}
			}
			else
			{
				this.buyButton.gameObject.SetActive(false);
				this.lockedButton.gameObject.SetActive(true);
				this.lockedButton.interactable = false;
				this.lockedButton.text.text = LM.Get("UI_SHOP_CHEST_0_WAIT");
			}
		}

		public Text header;

		public ButtonUpgradeAnim buyButton;

		public GameButton closeButton;

		public GameButton closeButtonBackground;

		public MenuShowCurrency menuShowCurrency;

		public Text body;

		public Image candyTreatImage;

		public Sprite[] candyTreatsImages;

		public RectTransform popupContent;

		public GameObject stockParent;

		public Text stockLabel;

		public GameButton lockedButton;

		public MenuShowCurrency candies;

		public RectTransform popupRect;
	}
}
