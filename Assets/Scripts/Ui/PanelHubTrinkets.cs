using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelHubTrinkets : AahMonoBehaviour
	{
		public void BounceSmithTabButton()
		{
			if (this.previousTween != null && this.previousTween.isPlaying)
			{
				this.previousTween.Kill(false);
			}
			this.buttonTabSmithing.transform.localScale = Vector3.one * 0.95f;
			this.previousTween = this.buttonTabSmithing.transform.DOScale(1f, 0.1f);
		}

		public void InitStrings()
		{
			this.headerText.text = LM.Get("UI_TRINKET");
			this.buttonTabTrinkets.text.text = LM.Get("UI_TRINKET");
			this.buttonTabSmithing.text.text = LM.Get("UI_SMITHING");
		}

		public Image backgroundImage;

		public GameButton buttonBack;

		public Text headerText;

		public RectTransform tabsParent;

		public HorizontalLayoutGroup tabsLayout;

		public GameButton buttonTabTrinkets;

		public GameButton buttonTabSmithing;

		public RectTransform trinketTabParent;

		public RectTransform trinketTabSelf;

		public RectTransform smithingTabParent;

		public MenuShowCurrency showCurrency;

		public MenuShowCurrency showCurrencySmithingTab;

		public Color trinketTabColor;

		public Color smithTabColor;

		public Image smithLockIcon;

		public bool isSmithingTab;

		private Tweener previousTween;
	}
}
