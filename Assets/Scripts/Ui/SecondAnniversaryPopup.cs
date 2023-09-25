using System;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class SecondAnniversaryPopup : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.closeButton.onClick = (this.backgroundButton.onClick = delegate()
			{
				this.manager.state = this.previousState;
				UiManager.AddUiSound(SoundArchieve.inst.uiPopupDisappear);
			});
			for (int i = 0; i < this.skinOfferPacks.Length; i++)
			{
				int ic = i;
				this.skinOfferPacks[i].button.gameButton.onClick = delegate()
				{
					this.manager.OnClickedShopSecondAnniversaryFlashOffer(ic);
				};
			}
			for (int j = 0; j < this.freeStuffPacks.Length; j++)
			{
				int ic = j + this.skinOfferPacks.Length;
				this.freeStuffPacks[j].button.gameButton.onClick = delegate()
				{
					this.manager.OnClickedShopSecondAnniversaryFlashOffer(ic);
				};
			}
		}

		public void InitStrings()
		{
			this.header.text = "SECOND_ANNIVERSARY_SHOP_AD_TITLE_2".Loc();
			this.skinOffersHeader.text = LM.Get("UI_SECOND_ANNIVERSARY_FLASH_OFFERS");
			this.freeStuffHeader.text = LM.Get("UI_SECOND_ANNIVERSARY_GIFTS_TITLE");
		}

		public void UpdatePopup(Simulator simulator)
		{
			EventConfig eventConfig = PlayfabManager.eventsInfo.GetEventConfig("secondAnniversary");
			if (eventConfig != null && TrustedTime.IsReady())
			{
				TimeSpan time = eventConfig.endDate - TrustedTime.Get();
				this.flashOffersTimeCounter.text = LM.Get("SHOP_PACK_TIME", GameMath.GetTimeDatailedShortenedString(time));
			}
			else
			{
				this.flashOffersTimeCounter.text = "UI_SHOP_CHEST_0_WAIT".Loc();
			}
			if (TrustedTime.IsReady() && simulator.specialOfferBoard.outOfShopOffers.Count > 0)
			{
				SpecialOfferKeeper specialOfferKeeper = simulator.specialOfferBoard.outOfShopOffers[0];
				if (UiManager.stateJustChanged)
				{
					ShopPackVisual shopPackVisual = this.manager.panelShop.GetShopPackVisual(specialOfferKeeper.offerPack);
					if (this.specialOfferWidget.packVisual == null)
					{
						this.specialOfferWidget.AddPackVisual(shopPackVisual);
						this.specialOfferWidget.onClick = new Action<int>(this.manager.OnClickedOutOfShopPackOffer);
					}
					else if (shopPackVisual.id != this.specialOfferWidget.packVisual.id)
					{
						this.specialOfferWidget.packVisual.DestoySelf();
						this.specialOfferWidget.AddPackVisual(shopPackVisual);
						this.specialOfferWidget.onClick = new Action<int>(this.manager.OnClickedOutOfShopPackOffer);
					}
					this.specialOfferWidget.header.text = specialOfferKeeper.offerPack.GetName();
					this.specialOfferWidget.offerIndex = 0;
					this.manager.panelShop.SetSpecialOfferVisualType(this.specialOfferWidget, false, false, true, false);
					if (specialOfferKeeper.offerPack.isIAP)
					{
						this.specialOfferWidget.price.SetCurrency(CurrencyType.GEM, specialOfferKeeper.offerPack.GetPriceString(), false, GameMode.RIFT, false);
						this.specialOfferWidget.price.SetTextX(0f);
					}
					else
					{
						this.specialOfferWidget.price.SetCurrency(specialOfferKeeper.offerPack.currency, specialOfferKeeper.offerPack.GetPriceString(), false, GameMode.STANDARD, true);
						this.specialOfferWidget.price.SetTextX(27.55f);
					}
					this.specialOfferWidget.discountBase.SetActive(specialOfferKeeper.offerPack.IsOfferFromOtherIap);
					if (specialOfferKeeper.offerPack.IsOfferFromOtherIap)
					{
						this.specialOfferWidget.originalPrice.text = specialOfferKeeper.offerPack.OriginalLocalizedPrice;
					}
					this.specialOfferWidget.SetContentText(specialOfferKeeper.offerPack, simulator.GetUniversalBonusAll());
				}
				TimeSpan remainingDur = specialOfferKeeper.GetRemainingDur(TrustedTime.Get());
				this.specialOfferWidget.timer.text = string.Format(LM.Get("SHOP_PACK_TIME"), GameMath.GetTimeDatailedShortenedString(remainingDur));
				this.specialOfferWidget.timer.rectTransform.sizeDelta = ((!specialOfferKeeper.offerPack.IsOfferFromOtherIap) ? SpecialOfferWidget.NormalTimerSize : SpecialOfferWidget.DiscountTimerSize);
				this.specialOfferWidget.gameObject.SetActive(true);
				this.flashOffersParent.SetAnchorPosY(this.flashOffersMidlePosition);
				this.contentParent.SetSizeDeltaY(this.contentHeightWithSpecialOffer);
			}
			else
			{
				this.specialOfferWidget.gameObject.SetActive(false);
				this.flashOffersParent.SetAnchorPosY(this.flashOffersTopPosition);
				this.contentParent.SetSizeDeltaY(this.contentHeightWithoutSpecialOffer);
			}
			ServerSideFlashOfferBundle secondAnniversaryFlashOffersBundle = simulator.secondAnniversaryFlashOffersBundle;
			if (secondAnniversaryFlashOffersBundle != null)
			{
				this.flashOffersParent.gameObject.SetActive(true);
				int i = 0;
				int count = secondAnniversaryFlashOffersBundle.offers.Count;
				while (i < count)
				{
					FlashOffer flashOffer = secondAnniversaryFlashOffersBundle.offers[i];
					FlashOfferWidget flashOfferWidget = (i >= this.skinOfferPacks.Length) ? this.freeStuffPacks[i - this.skinOfferPacks.Length] : this.skinOfferPacks[i];
					flashOfferWidget.SetOffer(flashOffer, this.manager, simulator.GetFlashOfferCurrencyType(flashOffer), simulator.GetFlashOfferCost(flashOffer), simulator.GetFlashOfferCount(flashOffer), simulator.GetFlashOfferPurchasesAllowedCount(flashOffer), simulator.IsFlashOfferLocked(flashOffer), simulator.GetFlashOfferUnlockDate(flashOffer));
					i++;
				}
			}
			else
			{
				this.flashOffersParent.gameObject.SetActive(false);
				this.contentParent.SetSizeDeltaY(0f);
			}
		}

		[SerializeField]
		private Text header;

		[SerializeField]
		private GameButton closeButton;

		[SerializeField]
		private GameButton backgroundButton;

		[SerializeField]
		private Text flashOffersTimeCounter;

		[SerializeField]
		private Text skinOffersHeader;

		[SerializeField]
		private FlashOfferWidget[] skinOfferPacks;

		[SerializeField]
		private Text freeStuffHeader;

		[SerializeField]
		private FlashOfferWidget[] freeStuffPacks;

		[SerializeField]
		private RectTransform flashOffersParent;

		[SerializeField]
		private float flashOffersMidlePosition;

		[SerializeField]
		private float flashOffersTopPosition;

		[SerializeField]
		private SpecialOfferWidget specialOfferWidget;

		[SerializeField]
		private RectTransform contentParent;

		[SerializeField]
		private float contentHeightWithSpecialOffer;

		[SerializeField]
		private float contentHeightWithoutSpecialOffer;

		[NonSerialized]
		public UiManager manager;

		[NonSerialized]
		public UiState previousState;
	}
}
