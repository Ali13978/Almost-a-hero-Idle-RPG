using System;
using System.Collections.Generic;
using DG.Tweening;
using Simulation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ui
{
	public class SpecialOfferWidget : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
	{
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

		public void SetContentText(ShopPack shopPack, UniversalTotalBonus universalTotalBonus)
		{
			this.strings.Clear();
			int numGears = shopPack.GetNumGears(universalTotalBonus);
			if (shopPack.numRunes > 0)
			{
				this.strings.Add(string.Format(LM.Get("UI_RUNES_PLURAL"), shopPack.numRunes));
			}
			if (numGears > 0)
			{
				this.strings.Add(AM.csScrap(numGears.ToString() + " " + LM.Get("UI_OFFER_ITEMS")));
			}
			if (shopPack.credits > 0.0)
			{
				this.strings.Add(AM.csGem(shopPack.credits.ToString() + " " + LM.Get("UI_GEMS")));
			}
			if (shopPack.candies > 0.0)
			{
				this.strings.Add(AM.csCandy(shopPack.candies.ToString() + " " + LM.Get("UI_CANDIES")));
			}
			if (shopPack.scrapsMax > 0.0)
			{
				this.strings.Add(AM.csScrap(shopPack.scrapsMax.ToString() + " " + LM.Get("UI_SCRAPS")));
			}
			if (shopPack.tokensMax > 0.0)
			{
				this.strings.Add(AM.csToken(shopPack.tokensMax.ToString() + " " + LM.Get("UI_TOKENS")));
			}
			if (shopPack.numTrinketPacks > 0)
			{
				this.strings.Add(string.Format(LM.Get("SHOP_PACK_FIVE_TRINKET_DESC"), AM.csToken(shopPack.numTrinketPacks.ToString())));
			}
			if (shopPack is ShopPackThreePijama)
			{
				this.strings.Add(string.Format(LM.Get("SHOP_PACK_THREE_PIJAMAS_DESC"), AM.cs(3.ToString(), "d64018")));
			}
			else if (shopPack is ShopPackStage100)
			{
				this.packVisual.extraLabelComponents[0].text = LM.Get("UI_SHOP_OFFER_TAG_TEXT");
				this.packVisual.extraLabelComponents[1].text = string.Format(LM.Get("UI_SHOP_OFFER_SIGN_TEXT"), 2);
			}
			else if (shopPack is ShopPackBigGem)
			{
				this.packVisual.extraLabelComponents[0].text = LM.Get("UI_SHOP_OFFER_TAG_TEXT");
				this.packVisual.extraLabelComponents[1].text = string.Format(LM.Get("UI_SHOP_OFFER_SIGN_TEXT"), 3);
			}
			else if (shopPack is ShopPackStage300 || shopPack is ShopPackStage800)
			{
				this.packVisual.extraLabelComponents[0].text = LM.Get("UI_SHOP_OFFER_TAG_TEXT");
				this.packVisual.extraLabelComponents[1].text = LM.Get("UI_SHOP_OFFER_SIGN_EXCLUSIVE");
			}
			else if (shopPack is ShopPackBigGemTwo)
			{
				this.packVisual.extraLabelComponents[0].text = LM.Get("UI_SHOP_OFFER_TAG_TEXT");
				this.packVisual.extraLabelComponents[1].text = string.Format(LM.Get("UI_SHOP_OFFER_SIGN_TEXT"), 4);
			}
			else if (shopPack is ShopPackRegional01)
			{
				this.packVisual.extraLabelComponents[0].text = string.Format(LM.Get("UI_DISCOUNT"), GameMath.GetPercentString(shopPack.discountPercentage, false));
			}
			else if (shopPack is ShopPackHalloweenGems)
			{
				this.packVisual.extraLabelComponents[0].text = LM.Get("SPECIAL_OFFER_LIMITED_TIME");
			}
			string text = string.Empty;
			int i = 0;
			int count = this.strings.Count;
			while (i < count)
			{
				text += this.strings[i];
				if (i < count - 1)
				{
					text += " + ";
				}
				i++;
			}
			this.content.text = text;
			float minFloat = GameMath.GetMinFloat(this.content.GetTextPreferredWidth(), this.rectTransform.rect.width * 0.954f - 60f);
			if (!minFloat.SigmaEqual(this.lastContentWidth))
			{
				this.content.rectTransform.SetSizeDeltaX(minFloat);
				this.contentParent.SetSizeDeltaX(minFloat + 60f);
				this.lastContentWidth = minFloat;
			}
		}

		public void AddPackVisual(ShopPackVisual prefab)
		{
			this.packVisual = UnityEngine.Object.Instantiate<ShopPackVisual>(prefab, this.prefabParent);
			this.packVisual.rectTransform.anchoredPosition = Vector2.zero;
			this.packVisual.foregroundVisualParent.SetParent(base.transform);
			this.packVisual.foregroundVisualParent.SetAsLastSibling();
			this.visualBackgroundImage.sprite = this.visualBackgrounds[this.packVisual.bacgroundIndex];
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (this.onClick != null && UiManager.secondsSinceLastButtonClick >= 0.1f)
			{
				UiManager.secondsSinceLastButtonClick = 0f;
				this.onClick(this.offerIndex);
			}
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (this.tweener != null && this.tweener.isPlaying)
			{
				this.tweener.Kill(false);
			}
			this.tweener = base.transform.DOScale(0.98f * this.defaultScale, 3f).SetSpeedBased<Tweener>();
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (this.tweener != null && this.tweener.isPlaying)
			{
				this.tweener.Kill(false);
			}
			this.tweener = base.transform.DOScale(1f * this.defaultScale, 3f).SetSpeedBased<Tweener>();
		}

		private void Awake()
		{
			this.defaultScale = this.rectTransform.localScale.x;
		}

		private RectTransform m_rectTransform;

		public Text header;

		public Text timer;

		public Text content;

		public Image frameImage;

		public Image headerImage;

		public Image contentBgImage;

		public Image buttonBgImage;

		public Image buttonImage;

		public Image buttonOutlineImage;

		public Image timerBackground;

		public Image visualBackgroundImage;

		public Sprite[] visualBackgrounds;

		public RectTransform contentParent;

		public MenuShowCurrency price;

		public GameObject discountBase;

		public Text originalPrice;

		public RectTransform prefabParent;

		[NonSerialized]
		public ShopPackVisual packVisual;

		public Action<int> onClick;

		public int offerIndex;

		public static readonly Vector2 NormalTimerSize = new Vector2(431f, 40f);

		public static readonly Vector2 DiscountTimerSize = new Vector2(280f, 40f);

		private Tweener tweener;

		private float lastContentWidth;

		private float defaultScale;

		private List<string> strings = new List<string>(12);
	}
}
