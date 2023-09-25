using System;
using System.Collections.Generic;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class FlashOfferWidget : MonoBehaviour
	{
		public void SetOffer(FlashOffer flashOffer, UiManager manager, CurrencyType? currencyRequired, double cost, int amount, int purchasesAllowed, bool locked, DateTime? unlockDate)
		{
			this.textPurchased.gameObject.SetActive(flashOffer.purchasesLeft == 0);
			this.buttonParent.gameObject.SetActive(flashOffer.purchasesLeft > 0);
			this.button.gameButton.interactable = (flashOffer.purchasesLeft > 0 && !locked);
			this.textPurchased.text = ((flashOffer.costType != FlashOffer.CostType.FREE) ? "UI_PURCHASED".Loc() : "UI_COLLECTED".Loc());
			if (flashOffer.purchasesLeft > 0)
			{
				this.button.iconDownType = ((currencyRequired == null) ? ButtonUpgradeAnim.IconType.NONE : ButtonUpgradeAnim.GetIconTypeFromCurrency(currencyRequired.Value));
				bool flag = locked || flashOffer.costType == FlashOffer.CostType.FREE || flashOffer.costType == FlashOffer.CostType.AD;
				if (this.noPriceText != null)
				{
					this.noPriceText.enabled = flag;
				}
				this.button.textDown.enabled = !flag;
				if (!flag)
				{
					this.button.textDown.text = GameMath.GetDoubleString(cost);
				}
				else if (locked)
				{
					this.noPriceText.text = "UI_LOCKED".Loc();
					this.noPriceText.color = this.button.textDownDisabledColor;
					this.noPriceTextShadow.enabled = false;
				}
				else
				{
					this.noPriceText.text = LM.Get((flashOffer.costType != FlashOffer.CostType.AD) ? "UI_SHOP_CHEST_0" : "UI_SHOP_AD");
					this.noPriceText.color = this.button.textDownEnabledColor;
					this.noPriceTextShadow.enabled = true;
				}
				if (this.textDupplicateCount != null)
				{
					this.textDupplicateCount.text = "x" + amount.ToString();
				}
				if (!locked && currencyRequired != null && !manager.sim.CanCurrencyAfford(currencyRequired.Value, cost))
				{
					this.button.textCantAffordColorChangeManual = true;
					this.button.isLevelUp = true;
				}
				else
				{
					this.button.textCantAffordColorChangeManual = false;
					this.button.isLevelUp = false;
				}
			}
			else if (this.textDupplicateCount != null)
			{
				this.textDupplicateCount.text = "x" + flashOffer.boughtAmount.ToString();
			}
			bool flag2 = flashOffer.type == FlashOffer.Type.CHARM;
			if (locked)
			{
				if (TrustedTime.IsReady() && unlockDate != null)
				{
					this.textName.text = "UI_SECOND_ANNIVERSARY_GIFT_TIMER".LocFormat(GameMath.GetTimeDatailedShortenedString(unlockDate.Value - TrustedTime.Get()));
				}
				else
				{
					this.textName.text = "UI_SHOP_CHEST_0_WAIT".Loc();
				}
			}
			else
			{
				this.textName.text = manager.GetFlashOfferName(flashOffer, true);
			}
			if (this.purchasesLeft != null)
			{
				this.purchasesLeft.enabled = (flashOffer.purchasesLeft > 1);
				if (flashOffer.purchasesLeft > 1)
				{
					this.purchasesLeft.text = string.Format(LM.Get("UI_MERCHANT_ITEM_HOWMANY"), flashOffer.purchasesLeft);
				}
			}
			bool flag3 = false;
			if (this.offerImageBackground != null)
			{
				if (locked)
				{
					this.offerImageBackground.enabled = false;
				}
				else
				{
					this.offerImageBackground.enabled = (flashOffer.type == FlashOffer.Type.RUNE || flashOffer.type == FlashOffer.Type.COSTUME || flashOffer.type == FlashOffer.Type.COSTUME_PLUS_SCRAP);
					if (this.skinPlus != null)
					{
						this.skinPlus.gameObject.SetActive(flashOffer.type == FlashOffer.Type.COSTUME_PLUS_SCRAP);
					}
					if (flashOffer.type == FlashOffer.Type.RUNE)
					{
						this.offerImageBackground.sprite = UiData.inst.runeOfferBackgroundSmall;
					}
					else if (flashOffer.type == FlashOffer.Type.COSTUME)
					{
						this.offerImageBackground.sprite = manager.GetHeroAvatar(flashOffer.genericIntId);
					}
					else if (flashOffer.type == FlashOffer.Type.COSTUME_PLUS_SCRAP)
					{
						SkinData skinData = manager.sim.GetSkinData(flashOffer.genericIntId);
						bool flag4 = manager.sim.IsHeroUnlocked(skinData.belongsTo.id);
						if (flag4)
						{
							this.offerImageBackground.sprite = manager.GetHeroAvatar(flashOffer.genericIntId);
						}
						else
						{
							this.offerImageBackground.enabled = false;
							flag3 = true;
						}
					}
					if (this.offerImageBackground.enabled)
					{
						this.offerImageBackground.rectTransform.anchoredPosition = ((!FlashOfferWidget.IconSettingsPerOffer.ContainsKey(flashOffer.type)) ? FlashOfferWidget.DefaultBackgroundPosition : FlashOfferWidget.IconSettingsPerOffer[flashOffer.type].BackgroundPosition);
						this.offerImageBackground.rectTransform.sizeDelta = ((!FlashOfferWidget.IconSettingsPerOffer.ContainsKey(flashOffer.type)) ? FlashOfferWidget.DefaultBackgroundSize : FlashOfferWidget.IconSettingsPerOffer[flashOffer.type].BackgroundSize);
					}
				}
			}
			if (locked)
			{
				this.offerImage.sprite = UiData.inst.giftBox;
				this.offerImage.rectTransform.anchoredPosition = Vector2.zero;
				this.offerImage.color = Color.white;
				this.offerImage.SetNativeSize();
				if (this.offerImageBackground_2 != null)
				{
					this.offerImageBackground_2.gameObject.SetActive(false);
				}
			}
			else if (flag2)
			{
				this.charmOfferIcon.gameObject.SetActive(true);
				this.charmOfferIcon.sprite = manager.GetFlashOfferImageSmall(flashOffer);
				CharmEffectData charmEffectData = manager.sim.allCharmEffects[flashOffer.charmId];
				this.offerImage.sprite = manager.GetCharmCardFlashOffer(charmEffectData.BaseData.charmType);
				this.charmOfferIcon.sprite = manager.spritesCharmEffectIcon[flashOffer.charmId];
			}
			else
			{
				if (this.offerImageBackground_2 != null)
				{
					if (flag3)
					{
						this.offerImageBackground_2.gameObject.SetActive(true);
						this.offerImage.sprite = UiData.inst.skinFramePassive;
					}
					else
					{
						this.offerImageBackground_2.gameObject.SetActive(false);
						this.offerImage.sprite = manager.GetFlashOfferImageSmall(flashOffer);
						this.offerImage.color = manager.GetFlashOfferImageColor(flashOffer);
					}
				}
				else
				{
					this.offerImage.sprite = manager.GetFlashOfferImageSmall(flashOffer);
					this.offerImage.color = manager.GetFlashOfferImageColor(flashOffer);
				}
				this.offerImage.rectTransform.anchoredPosition = ((!FlashOfferWidget.IconSettingsPerOffer.ContainsKey(flashOffer.type)) ? FlashOfferWidget.DefaultIconPosition : FlashOfferWidget.IconSettingsPerOffer[flashOffer.type].IconPosition);
				this.offerImage.rectTransform.sizeDelta = ((!FlashOfferWidget.IconSettingsPerOffer.ContainsKey(flashOffer.type)) ? FlashOfferWidget.DefaultIconSize : FlashOfferWidget.IconSettingsPerOffer[flashOffer.type].IconSize);
			}
		}

		private void Awake()
		{
			if (this.noPriceText != null)
			{
				this.noPriceTextShadow = this.noPriceText.GetComponent<Shadow>();
			}
		}

		public Image offerImage;

		public Image offerImageBackground;

		public Image offerImageBackground_2;

		public Text textName;

		public Text textPurchased;

		public Text purchasesLeft;

		public ButtonUpgradeAnim button;

		public RectTransform buttonParent;

		public Text noPriceText;

		[Header("Charm Specific")]
		public Text textDupplicateCount;

		public Image charmOfferIcon;

		public Image skinPlus;

		private Shadow noPriceTextShadow;

		private static readonly Vector2 DefaultIconPosition = new Vector2(0f, -1.9f);

		private static readonly Vector2 DefaultIconSize = new Vector2(156f, 156f);

		private static readonly Vector2 DefaultBackgroundPosition = new Vector2(0f, -1.9f);

		private static readonly Vector2 DefaultBackgroundSize = new Vector2(156f, 156f);

		private static readonly Dictionary<FlashOffer.Type, FlashOfferWidget.IconSettings> IconSettingsPerOffer = new Dictionary<FlashOffer.Type, FlashOfferWidget.IconSettings>
		{
			{
				FlashOffer.Type.RUNE,
				new FlashOfferWidget.IconSettings
				{
					IconPosition = new Vector2(0f, 3.8f),
					IconSize = new Vector2(84f, 84f),
					BackgroundPosition = new Vector2(0f, 3.8f),
					BackgroundSize = new Vector2(116f, 116f)
				}
			},
			{
				FlashOffer.Type.GEM,
				new FlashOfferWidget.IconSettings
				{
					IconPosition = new Vector2(-3f, 7.3f),
					IconSize = new Vector2(218f, 218f)
				}
			},
			{
				FlashOffer.Type.COSTUME,
				new FlashOfferWidget.IconSettings
				{
					IconPosition = new Vector2(0f, 5.07f),
					IconSize = new Vector2(130f, 130f),
					BackgroundPosition = new Vector2(4.1f, 6.3f),
					BackgroundSize = new Vector2(108f, 108f)
				}
			},
			{
				FlashOffer.Type.COSTUME_PLUS_SCRAP,
				new FlashOfferWidget.IconSettings
				{
					IconPosition = new Vector2(0f, 5.07f),
					IconSize = new Vector2(130f, 130f),
					BackgroundPosition = new Vector2(4.1f, 6.3f),
					BackgroundSize = new Vector2(108f, 108f)
				}
			}
		};

		private class IconSettings
		{
			public Vector2 IconPosition;

			public Vector2 IconSize;

			public Vector2 BackgroundPosition;

			public Vector2 BackgroundSize;
		}
	}
}
