using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelAdPopup : AahMonoBehaviour
	{
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
			this.buttonCancel.text.text = LM.Get("UI_CANCEL");
			this.buttonCollect.text.text = LM.Get("UI_COLLECT");
			this.buttonWatch.text.text = LM.Get("UI_AD_WATCH");
		}

		public void SetDetails(CurrencyType currencyType, double amount, bool isAdWatched)
		{
			if (isAdWatched)
			{
				this.buttonWatch.gameObject.SetActive(false);
				this.buttonCancel.gameObject.SetActive(false);
				this.buttonCollect.gameObject.SetActive(true);
				if (currencyType == CurrencyType.GOLD)
				{
					this.textTitle.text = LM.Get("UI_AD_COLLECT_GOLD");
				}
				else if (currencyType == CurrencyType.GEM)
				{
					this.textTitle.text = LM.Get("UI_AD_COLLECT_GEMS");
				}
				else if (currencyType == CurrencyType.SCRAP)
				{
					this.textTitle.text = LM.Get("UI_AD_COLLECT_SCRAPS");
				}
				else if (currencyType == CurrencyType.TOKEN)
				{
					this.textTitle.text = LM.Get("UI_AD_COLLECT_TOKENS");
				}
				else
				{
					if (currencyType != CurrencyType.CANDY)
					{
						throw new NotImplementedException();
					}
					this.textTitle.text = LM.Get("UI_AD_COLLECT_CANDIES");
				}
				this.cartReward.SetCurrency(currencyType);
				this.textOffer.text = LM.Get("UI_AD_WATCHED_DESC");
			}
			else
			{
				this.buttonWatch.gameObject.SetActive(true);
				this.buttonCancel.gameObject.SetActive(true);
				this.buttonCollect.gameObject.SetActive(false);
				this.textTitle.text = LM.Get("UI_AD_TITLE");
				if (currencyType == CurrencyType.GEM)
				{
					this.textOffer.text = LM.Get("UI_AD_WATCH_DESC_GEMS");
				}
				else if (currencyType == CurrencyType.GOLD)
				{
					this.textOffer.text = LM.Get("UI_AD_WATCH_DESC_GOLD");
				}
				else if (currencyType == CurrencyType.SCRAP)
				{
					this.textOffer.text = LM.Get("UI_AD_WATCH_DESC_SCRAPS");
				}
				else
				{
					if (currencyType != CurrencyType.TOKEN)
					{
						throw new NotImplementedException();
					}
					this.textOffer.text = LM.Get("UI_AD_WATCH_DESC_TOKENS");
				}
				this.cartReward.SetCurrency(currencyType);
			}
			this.menuShowCurrency.SetCurrency(currencyType, GameMath.GetDoubleString(amount), false, GameMode.STANDARD, true);
		}

		public GameButton buttonWatch;

		public GameButton buttonCancel;

		public GameButton buttonCollect;

		public Text textTitle;

		public Text textOffer;

		public MenuShowCurrency menuShowCurrency;

		public CartWidget cartReward;

		public Sprite spriteCancelGold;

		public Sprite spriteCancelCredit;

		public Color colorOfferGold;

		public Color colorOfferCredit;

		public Color colorCancelGold;

		public Color colorCancelCredit;

		public Color colorCancelOutlineGold;

		public Color colorCancelOutlineCredit;

		public Color colorTitleGold;

		public Color colorTitleCredit;

		public float timer;

		public RectTransform popupRect;
	}
}
