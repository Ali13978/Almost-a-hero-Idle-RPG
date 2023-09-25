using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class TrinketPackPopup : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.InitStrings();
			this.buyButtonsXpos = this.buyWithGemButton.rectTransform.anchoredPosition.x;
		}

		public void InitStrings()
		{
			this.orLabel.text = LM.Get("UI_OR");
			this.description.text = LM.Get("UI_TRINKET_OPENING_DESC");
			this.header.text = LM.Get("SHOP_PACK_TRINKET");
			this.openButton.text.text = LM.Get("UI_MERCHANT_SELECT_BUY");
			this.buyWithGemButton.textDown.text = LM.Get("UI_BUY");
			this.buyWithAeonButton.textDown.text = LM.Get("UI_BUY");
		}

		public GameButton openButton;

		public GameButton closeBgButton;

		public GameButton closeCrossButton;

		public ButtonUpgradeAnim buyWithGemButton;

		public ButtonUpgradeAnim buyWithAeonButton;

		public Text description;

		public Text header;

		public Text stockCountLabel;

		public Text orLabel;

		public RectTransform popupRect;

		[NonSerialized]
		public float buyButtonsXpos;
	}
}
