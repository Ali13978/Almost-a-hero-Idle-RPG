using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelSkinBuyingPopup : AahMonoBehaviour
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
			this.header.text = LM.Get("UI_SKIN");
			this.buttonNo.text.text = LM.Get("UI_CANCEL");
			this.buttonYes.textDown.text = LM.Get("UI_BUY");
		}

		public UiState oldSkinPopupState;

		public RectTransform popupParent;

		public HeroAnimation heroAnimation;

		public Text header;

		public Text heroName;

		public GameButton buttonNo;

		public ButtonUpgradeAnim buttonYes;

		public RectTransform halloweenScrap;

		public RectTransform halloweenScrapPlus;
	}
}
