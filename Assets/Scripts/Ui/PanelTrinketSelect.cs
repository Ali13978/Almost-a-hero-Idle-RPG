using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelTrinketSelect : MonoBehaviour
	{
		public void InitString()
		{
			this.trinketsTabLabel.text = LM.Get("UI_TAB_TRINKETS");
			this.smithingTabLabel.text = LM.Get("UI_SMITHING");
		}

		public Image background;

		public RectTransform tabButtonsParent;

		public GameButton buttonTabTrinkets;

		public GameButton buttonTabSmith;

		public RectTransform trinketScrollerParent;

		public RectTransform trinketSmitherParent;

		public Color trinketTabColor;

		public Color smithTabColor;

		public Image buttonSmithLock;

		public MenuShowCurrency menuShowCurrencyScraps;

		public MenuShowCurrency menuShowCurrencyGems;

		public Text trinketsTabLabel;

		public Text smithingTabLabel;

		[NonSerialized]
		public bool isSmithing;
	}
}
