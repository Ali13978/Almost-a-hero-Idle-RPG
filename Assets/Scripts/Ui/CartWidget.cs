using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class CartWidget : MonoBehaviour
	{
		public void SetCurrency(CurrencyType currencyType)
		{
			UiData inst = UiData.inst;
			switch (currencyType)
			{
			case CurrencyType.GOLD:
				this.imageCurrency1.sprite = inst.cartCurrencyGoldBig;
				this.imageCurrency2.sprite = inst.cartCurrencyGoldSmall;
				break;
			case CurrencyType.SCRAP:
				this.imageCurrency1.sprite = inst.cartCurrencyScrapsBig;
				this.imageCurrency2.sprite = inst.cartCurrencyScrapsSmall;
				break;
			case CurrencyType.MYTHSTONE:
				this.imageCurrency1.sprite = inst.cartCurrencyMythstoneBig;
				this.imageCurrency2.sprite = inst.cartCurrencyMythstoneSmall;
				break;
			case CurrencyType.GEM:
				this.imageCurrency1.sprite = inst.cartCurrencyCreditsBig;
				this.imageCurrency2.sprite = inst.cartCurrencyCreditsSmall;
				break;
			case CurrencyType.TOKEN:
				this.imageCurrency1.sprite = inst.cartCurrencyTokensBig;
				this.imageCurrency2.sprite = inst.cartCurrencyTokensSmall;
				break;
			case CurrencyType.AEON:
				this.imageCurrency1.sprite = inst.cartCurrencyAeonsBig;
				this.imageCurrency2.sprite = inst.cartCurrencyAeonsSmall;
				break;
			default:
				throw new NotImplementedException("Currency type: " + currencyType);
			}
		}

		[SerializeField]
		private Image imageCurrency1;

		[SerializeField]
		private Image imageCurrency2;
	}
}
