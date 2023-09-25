using System;
using System.Collections.Generic;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelShop : AahMonoBehaviour
	{
		public bool IsHoldingAScrollPosition()
		{
			return this.scrollPositionTimer < this.scrollPositionCooldown;
		}

		public RectTransform rectTransform
		{
			get
			{
				RectTransform result;
				if ((result = this.m_rectTransform) == null)
				{
					result = (this.m_rectTransform = base.GetComponent<RectTransform>());
				}
				return result;
			}
		}

		public ChildrenContentSizeFitter childrenContentSizeFitter
		{
			get
			{
				ChildrenContentSizeFitter result;
				if ((result = this.m_childrenContentSizeFitter) == null)
				{
					result = (this.m_childrenContentSizeFitter = this.scrollView.content.GetComponent<ChildrenContentSizeFitter>());
				}
				return result;
			}
		}

		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.InitStrings();
			this.visualStarter.id = 0;
			this.visualRune.id = 2;
			this.visualToken.id = 3;
			this.visualCurrency.id = 4;
			this.visualTrinkets.id = 5;
			this.visualXmas.id = 6;
			this.visualBigGem.id = 8;
			this.visualRiftPack0.id = 9;
			this.visualRiftPack1.id = 10;
			this.visualRiftPack2.id = 11;
			this.visualRegionalPack0.id = 12;
			this.visualStage100.id = 13;
			this.visualHalloweenGemsPack.id = 14;
			this.visualChristmasGemsSmallPack.id = 15;
			this.visualChristmasGemsBigPack.id = 16;
			this.visualSecondAnniversaryGems.id = 17;
			this.visualSecondAnniversaryCurrencyBundle.id = 18;
		}

		public ShopPackVisual GetShopPackVisual(ShopPack shopPack)
		{
			if (shopPack is ShopPackStarter)
			{
				return this.visualStarter;
			}
			if (shopPack is ShopPackRune)
			{
				return this.visualRune;
			}
			if (shopPack is ShopPackToken)
			{
				return this.visualToken;
			}
			if (shopPack is ShopPackCurrency)
			{
				return this.visualCurrency;
			}
			if (shopPack is ShopPackFiveTrinkets)
			{
				return this.visualTrinkets;
			}
			if (shopPack is ShopPackXmas)
			{
				return this.visualXmas;
			}
			if (shopPack is ShopPackThreePijama)
			{
				throw new MissingComponentException("This visual has been removed");
			}
			if (shopPack is ShopPackBigGem || shopPack is ShopPackBigGemTwo || shopPack is ShopPackStage300)
			{
				return this.visualBigGem;
			}
			if (shopPack is ShopPackStage100 || shopPack is ShopPackStage800)
			{
				return this.visualStage100;
			}
			if (shopPack is ShopPackRiftOffer01)
			{
				return this.visualRiftPack0;
			}
			if (shopPack is ShopPackRiftOffer02)
			{
				return this.visualRiftPack1;
			}
			if (shopPack is ShopPackRiftOffer03)
			{
				return this.visualRiftPack2;
			}
			if (shopPack is ShopPackRiftOffer04)
			{
				return this.visualRiftPack2;
			}
			if (shopPack is ShopPackRegional01)
			{
				return this.visualRegionalPack0;
			}
			if (shopPack is ShopPackHalloweenGems)
			{
				return this.visualHalloweenGemsPack;
			}
			if (shopPack is ShopPackChristmasGemsSmall)
			{
				return this.visualChristmasGemsSmallPack;
			}
			if (shopPack is ShopPackChristmasGemsBig)
			{
				return this.visualChristmasGemsBigPack;
			}
			if (shopPack is ShopPackSecondAnniversaryGems || shopPack is ShopPackSecondAnniversaryGemsTwo)
			{
				return this.visualSecondAnniversaryGems;
			}
			if (shopPack is ShopPackSecondAnniversaryCurrencyBundle || shopPack is ShopPackSecondAnniversaryCurrencyBundleTwo)
			{
				return this.visualSecondAnniversaryCurrencyBundle;
			}
			throw new Exception("Can not find visual for pack: " + shopPack);
		}

		public bool IsTabChanged()
		{
			return this.isLookingAtOffers != this.wasLookingAtOffers;
		}

		public void InitStrings()
		{
			this.buttonTabVault.text.text = LM.Get("UI_SHOP_VAULT");
			this.buttonTabOffers.text.text = LM.Get("UI_SHOP_OFFERS");
			this.textTitle0.text = LM.Get("UI_PACKS_SECTION_TITLE");
			this.textTitle2.text = LM.Get("UI_SHOP_GEM_PACKS");
			this.textTitleMines.text = LM.Get("UI_MINES");
			this.riftPackTitle.text = LM.Get("UI_GATES_OFFERS_TITLE");
			this.adventureOffersTitle.text = LM.Get("UI_ADVENTURE_OFFERS_TITLE");
			this.socialRewardsSectionTitle.text = LM.Get("SOCIAL_REWARS_SECTION_TITLE");
			this.shopLootPacks[0].textTitle.text = LM.Get("UI_SHOP_CHEST_0");
			this.shopLootPacks[1].textTitle.text = LM.Get("UI_SHOP_CHEST_1");
			this.shopLootPacks[2].textTitle.text = LM.Get("UI_SHOP_CHEST_2");
			this.shopCharmPacks[0].textTitle.text = LM.Get("CHARM_PACK");
			this.shopCharmPacks[1].textTitle.text = LM.Get("CHARM_PACK_BIG");
			int i = 0;
			int num = this.panelBuyCredits.Length;
			while (i < num)
			{
				this.panelBuyCredits[i].buttonUpgradeAnim.textUp.text = Simulator.CREDIT_PACKS_AMOUNT[i + 1].ToString() + " " + LM.Get("UI_GEMS");
				i++;
			}
			this.textsMore[2].text = string.Format(LM.Get("UI_SHOP_MORE"), GameMath.GetPercentString(0.1f, false));
			this.textsMore[3].text = string.Format(LM.Get("UI_SHOP_MORE"), GameMath.GetPercentString(0.2f, false));
			this.textsMore[4].text = string.Format(LM.Get("UI_SHOP_MORE"), GameMath.GetPercentString(0.3f, false));
			this.textsMore[5].text = string.Format(LM.Get("UI_SHOP_MORE"), GameMath.GetPercentString(0.4f, false));
			this.panelTrinket.buttonUpgradeAnim.textUp.text = LM.Get("SHOP_PACK_TRINKET");
			this.panelMineToken.textTitle.text = LM.Get("MINE_TOKEN");
			this.panelMineScrap.textTitle.text = LM.Get("MINE_SCRAP");
			this.mineTokensLocked.text = LM.Get("UI_LOCKED");
			this.flashOfferHeader.text = LM.Get("SHOP_FLASH_OFFER");
			this.specialOffersSectionTitle.text = LM.Get("UI_SPECIAL_OFFERS");
			this.halloweenHeader.text = LM.Get("UI_HALLOWEEN");
			this.secondAnniversaryOfferTitle.text = "SECOND_ANNIVERSARY_SHOP_AD_TITLE".Loc();
			this.secondAnniversaryOfferDesc.text = "SECOND_ANNIVERSARY_SHOP_AD_DESC".Loc();
		}

		public void HideTabbar()
		{
			this.tabbarParent.gameObject.SetActive(false);
			this.scrollView.viewport.SetTopDelta(0f);
		}

		public void SetSpecialOfferVisualType(SpecialOfferWidget widget, bool isGogOffer, bool isRegional, bool isSeasonal, bool isStarterPack)
		{
			if (isRegional)
			{
				widget.frameImage.sprite = this.regionalSpecialFrame;
				widget.headerImage.sprite = this.regionalSpecialHeader;
				widget.buttonBgImage.sprite = this.regionalSpecialButtonBg;
				widget.contentBgImage.color = this.regionalContentBgColor;
				widget.buttonOutlineImage.color = this.regionalButtonBgColor;
				widget.timerBackground.color = this.regionalDurationBgColor;
			}
			else if (isGogOffer)
			{
				widget.frameImage.sprite = this.riftSpecialFrame;
				widget.headerImage.sprite = this.riftSpecialHeader;
				widget.buttonBgImage.sprite = this.riftSpecialButtonBg;
				widget.contentBgImage.color = this.riftContentBgColor;
				widget.buttonOutlineImage.color = this.riftButtonBgColor;
				widget.timerBackground.color = this.riftDurationBgColor;
			}
			else if (isSeasonal)
			{
				widget.frameImage.sprite = this.seasonalSpecialFrame;
				widget.headerImage.sprite = this.seasonalSpecialHeader;
				widget.buttonBgImage.sprite = this.seasonalSpecialButtonBg;
				widget.contentBgImage.color = this.seasonalContentBgColor;
				widget.buttonOutlineImage.color = this.seasonalButtonBgColor;
				widget.timerBackground.color = this.seasonalDurationBgColor;
			}
			else if (isStarterPack)
			{
				widget.frameImage.sprite = this.starterSpecialFrame;
				widget.headerImage.sprite = this.starterSpecialHeader;
				widget.buttonBgImage.sprite = this.starterSpecialButtonBg;
				widget.contentBgImage.color = this.starterContentBgColor;
				widget.buttonOutlineImage.color = this.starterButtonBgColor;
				widget.timerBackground.color = this.starterDurationBgColor;
			}
			else
			{
				widget.frameImage.sprite = this.normalSpecialFrame;
				widget.headerImage.sprite = this.normalSpecialHeader;
				widget.buttonBgImage.sprite = this.normalSpecialButtonBg;
				widget.contentBgImage.color = this.normalContentBgColor;
				widget.buttonOutlineImage.color = this.normalButtonBgColor;
				widget.timerBackground.color = this.normalDurationBgColor;
			}
		}

		internal void ResetScrollPosition()
		{
			this.scrollView.content.SetAnchorPosY(0f);
			this.focusOnFlashOffers = false;
		}

		public Text textTitle0;

		public Text textTitle2;

		public Text textTitleMines;

		public Text[] textsMore;

		public RectTransform tabbarParent;

		public NotificationBadge offersNotificationBadge;

		[Header("Vault")]
		public MerchantItem[] shopLootPacks;

		public MerchantItem panelFreeCredits;

		public MerchantItem[] panelBuyCredits;

		public RectTransform parentLootpacks;

		public RectTransform parentTrinketPack;

		public RectTransform parentGemPacks;

		public RectTransform parentMines;

		public MerchantItem panelTrinket;

		public RectTransform goblinIcon;

		public NotificationBadge trinketStockNotification;

		public MerchantItem panelMineToken;

		public MerchantItem panelMineScrap;

		public RectTransform[] rectTransformLocked;

		public Text mineTokensLocked;

		[HideInInspector]
		public bool forceGoblinAnimation;

		[Header("Offers")]
		public Text socialRewardsSectionTitle;

		public MerchantItem[] socialRewardOffers;

		public RectTransform parentSpecialOffer;

		public RectTransform parentSocialOffers;

		public ShopPackVisual visualStarter;

		public ShopPackVisual visualRune;

		public ShopPackVisual visualToken;

		public ShopPackVisual visualCurrency;

		public ShopPackVisual visualTrinkets;

		public ShopPackVisual visualXmas;

		public ShopPackVisual visualBigGem;

		public ShopPackVisual visualStage100;

		public ShopPackVisual visualRiftPack0;

		public ShopPackVisual visualRiftPack1;

		public ShopPackVisual visualRiftPack2;

		public ShopPackVisual visualRegionalPack0;

		public ShopPackVisual visualHalloweenGemsPack;

		public ShopPackVisual visualChristmasGemsSmallPack;

		public ShopPackVisual visualChristmasGemsBigPack;

		public ShopPackVisual visualSecondAnniversaryGems;

		public ShopPackVisual visualSecondAnniversaryCurrencyBundle;

		public RectTransform secondAnniversaryOfferParent;

		public GameButton secondAnniversaryOfferButton;

		public Text secondAnniversaryOfferTitle;

		public Text secondAnniversaryOfferDesc;

		public Image secondAnniversaryOfferNotif;

		[Header("Special Offers")]
		public Text specialOffersSectionTitle;

		public Color32 normalButtonBgColor;

		public Color32 normalContentBgColor;

		public Color32 normalDurationBgColor;

		public Sprite normalSpecialFrame;

		public Sprite normalSpecialHeader;

		public Sprite normalSpecialButtonBg;

		public Color32 riftButtonBgColor;

		public Color32 riftContentBgColor;

		public Color32 riftDurationBgColor;

		public Sprite riftSpecialFrame;

		public Sprite riftSpecialHeader;

		public Sprite riftSpecialButtonBg;

		public Color32 regionalButtonBgColor;

		public Color32 regionalContentBgColor;

		public Color32 regionalDurationBgColor;

		public Sprite regionalSpecialFrame;

		public Sprite regionalSpecialHeader;

		public Sprite regionalSpecialButtonBg;

		public Color32 seasonalButtonBgColor;

		public Color32 seasonalContentBgColor;

		public Color32 seasonalDurationBgColor;

		public Sprite seasonalSpecialFrame;

		public Sprite seasonalSpecialHeader;

		public Sprite seasonalSpecialButtonBg;

		public Color32 starterButtonBgColor;

		public Color32 starterContentBgColor;

		public Color32 starterDurationBgColor;

		public Sprite starterSpecialFrame;

		public Sprite starterSpecialHeader;

		public Sprite starterSpecialButtonBg;

		public SpecialOfferWidget specialOfferWidgetPrefab;

		public List<SpecialOfferWidget> specialOfferWidgets;

		public MenuShowCurrency currencyShopPackPrice;

		[HideInInspector]
		public bool timeWasReady;

		[HideInInspector]
		public bool thereWasTutorial;

		[HideInInspector]
		public bool forceUpdatePackOffer;

		public Text signText;

		public Text tagText;

		public Color colorBlue;

		public Color colorGreen;

		public Image imageShopPackBuy;

		public Sprite ShopBackgroundDefault;

		public Sprite shopBackgroundFlipped;

		public ScrollRect scrollView;

		public GameButton buttonTabVault;

		public GameButton buttonTabOffers;

		[Header("Flash Offers")]
		public RectTransform parentFlashOffers;

		[Header("Adventure Flash Offers")]
		public Text adventureOffersTitle;

		public RectTransform parentAdventureFlashOffers;

		public FlashOfferWidget[] adventureFlashOfferPacks;

		[Header("Rift")]
		public RectTransform parentRiftPack;

		public MerchantItem[] shopCharmPacks;

		public FlashOfferWidget[] flashOfferPacks;

		public Text riftPackTitle;

		public Text flashOfferHeader;

		public RectTransform timerTrayParent;

		public Text flashOfferEndsIn;

		[Header("Halloween")]
		public RectTransform parentHalloweenFlashOffers;

		public Text halloweenOfferTimeCounter;

		public Text halloweenHeader;

		public FlashOfferWidget[] halloweenFlashOfferPacks;

		private RectTransform m_rectTransform;

		[HideInInspector]
		public bool isHubMode;

		[HideInInspector]
		public float previousYPos;

		[NonSerialized]
		public bool focusOnFlashOffers;

		[NonSerialized]
		public bool isLookingAtOffers;

		public bool wasLookingAtOffers = true;

		[NonSerialized]
		public float scrollPositionKeeper = 1f;

		[NonSerialized]
		public float scrollSizeKeeper;

		[NonSerialized]
		public float scrollPositionCooldown = 5f;

		[NonSerialized]
		public float scrollPositionTimer = 5f;

		private ChildrenContentSizeFitter m_childrenContentSizeFitter;

		public bool setSpecialOfferContentTexts;
	}
}
