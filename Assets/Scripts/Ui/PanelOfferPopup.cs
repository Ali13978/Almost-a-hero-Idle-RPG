using System;
using System.Collections.Generic;
using DG.Tweening;
using DynamicLoading;
using PlayFab.ClientModels;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelOfferPopup : AahMonoBehaviour
	{
		public void SetOffer(ShopPack offerPack)
		{
			if (offerPack is ShopPackRegionalBase)
			{
				this.discountText.text = string.Format(LM.Get("UI_DISCOUNT"), GameMath.GetPercentString(offerPack.discountPercentage, false));
				this.textHeader.text = offerPack.GetName();
				this.originalPrice.text = offerPack.OriginalLocalizedPrice;
				this.price.text = offerPack.GetPriceString();
				this.textBodyUp.text = string.Format(LM.Get("UI_REGIONAL_OFFER_1_DESC_2"), LM.Get(this.CountryNameKeys[(offerPack as ShopPackRegionalBase).countryCodes[0]]));
				this.textBodyDown.text = LM.Get("UI_REGIONAL_OFFER_1_DESC_1");
				this.limitedTimeText.gameObject.SetActive(false);
				this.headerBackground.color = this.colorRegional;
			}
			else if (offerPack is ShopPackHalloweenGems)
			{
				this.discountText.gameObject.SetActive(false);
				this.textHeader.text = offerPack.GetName();
				this.textBodyUp.transform.SetScale(0f);
				this.textBodyUp.transform.DOScale(1f, 0.4f).SetEase(Ease.OutBack);
				this.textBodyDown.transform.SetScale(0f);
				this.textBodyDown.transform.DOScale(1f, 0.4f).SetEase(Ease.OutBack).SetDelay(0.2f);
				this.textBodyUp.text = LM.Get("HALLOWEEN_OFFER_POPUP_0");
				this.textBodyDown.text = LM.Get("HALLOWEEN_OFFER_POPUP_1");
				this.limitedTimeText.gameObject.SetActive(true);
				this.headerBackground.color = this.colorHalloween;
				this.halloweenPrafabBundle.LoadObjectAsync(new Action<GameObject>(this.OnHalloweenArtLoaded));
				this.limitedTimeText.transform.SetScale(0f);
				this.unloadEvent = delegate()
				{
					if (this.halloweenArtInstance != null)
					{
						UnityEngine.Object.Destroy(this.halloweenArtInstance);
						this.halloweenArtInstance = null;
					}
					this.halloweenPrafabBundle.Unload();
				};
			}
		}

		private void OnHalloweenArtLoaded(GameObject obj)
		{
			if (this.halloweenArtInstance != null)
			{
				UnityEngine.Object.Destroy(this.halloweenArtInstance);
			}
			if (obj == null)
			{
				return;
			}
			this.halloweenArtInstance = UnityEngine.Object.Instantiate<GameObject>(obj, this.artParent);
			this.limitedTimeText.text = LM.Get("SPECIAL_OFFER_LIMITED_TIME");
			this.halloweenArtInstance.transform.SetScale(0f);
			this.halloweenArtInstance.transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack);
			this.limitedTimeText.transform.SetScale(0f);
			this.limitedTimeText.transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack);
			this.limitedTimeText.transform.SetAsLastSibling();
		}

		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.InitStrings();
		}

		public void InitStrings()
		{
			this.buttonYes.text.text = LM.Get("UI_GO_TO_SHOP");
		}

		public void OnClose()
		{
			if (this.unloadEvent != null)
			{
				this.unloadEvent();
			}
		}

		public Text textHeader;

		public Text textBodyUp;

		public Text textBodyDown;

		public Text price;

		public Text originalPrice;

		public Image headerBackground;

		public GameButton buttonClose;

		public GameButton buttonYes;

		public Text discountText;

		public Text limitedTimeText;

		public RectTransform artParent;

		public RectTransform popupRect;

		public Color colorRegional;

		public Color colorHalloween;

		public BPrefab halloweenPrafabBundle;

		private GameObject halloweenArtInstance;

		private Action unloadEvent;

		private Dictionary<CountryCode, string> CountryNameKeys = new Dictionary<CountryCode, string>
		{
			{
				CountryCode.MX,
				"UI_MEXICO"
			},
			{
				CountryCode.BR,
				"UI_BRAZIL"
			},
			{
				CountryCode.ES,
				"ESPA�A"
			}
		};
	}
}
