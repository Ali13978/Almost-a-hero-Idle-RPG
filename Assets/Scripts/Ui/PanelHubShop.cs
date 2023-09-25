using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelHubShop : MonoBehaviour
	{
		public void InitStrings()
		{
			this.header.text = LM.Get("UI_TAB_SHOP");
		}

		public void MoveScrollTo(float normalizedPosition)
		{
			this.panelShop.scrollView.verticalNormalizedPosition = normalizedPosition;
		}

		public PanelShop panelShop;

		public GameButton buttonBack;

		public Text header;

		public RectTransform shopTargetParent;

		public MenuShowCurrency menuShowCurrencyCredits;

		public MenuShowCurrency menuShowCurrencyAeons;

		public Transform headerParent;
	}
}
