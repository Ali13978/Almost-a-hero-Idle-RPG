using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelMerchantItemSelect : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.InitStrings();
			this.descStartPos = this.textDesciption0.rectTransform.anchoredPosition;
			this.descSecondaryStartPos = this.textDesciptionSecondary.rectTransform.anchoredPosition;
			this.descMidPos = (this.descStartPos + this.descSecondaryStartPos) * 0.5f;
		}

		public void SetSingleDescription()
		{
			this.textDesciptionSecondary.gameObject.SetActive(false);
			this.textDesciption0.rectTransform.anchoredPosition = this.descMidPos;
		}

		public void SetTwoDescription()
		{
			this.textDesciptionSecondary.gameObject.SetActive(true);
			this.textDesciption0.rectTransform.anchoredPosition = this.descStartPos;
		}

		public void InitStrings()
		{
			this.buttonBuy.textDown.text = LM.Get("UI_MERCHANT_SELECT_BUY");
		}

		public Image icon;

		public Text textTitle;

		public Text textDesciption0;

		public Text textDesciptionSecondary;

		public Text textDesciption1;

		public ButtonUpgradeAnim buttonBuy;

		public GameButton buttonClose;

		public GameButton buttonCloseCross;

		public MenuShowCurrency currencyWidget;

		public GameButton useItemButton;

		public Text nonLeftMessage;

		public RectTransform contentParent;

		public RectTransform multiBuyParent;

		public Text textOpen;

		public GameButton buttonInreaseCopy;

		public GameButton buttonDecreaseCopy;

		private Vector2 descStartPos;

		private Vector2 descMidPos;

		private Vector2 descSecondaryStartPos;

		public int copyAmount;
	}
}
