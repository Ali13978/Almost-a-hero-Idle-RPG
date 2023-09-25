using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelTrinketInfoPopup : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.trinketInfoBody.Init();
			this.destroyRewardText.color = Color.white;
		}

		public void InitStrings()
		{
			this.header.text = LM.Get("UI_TRINKET");
			if (this.buttonEquip != null)
			{
				this.buttonEquip.text.text = LM.Get("UI_EQUIP");
			}
			this.trinketInfoBody.InitStrings();
		}

		public TrinketInfoBody trinketInfoBody;

		public Text header;

		public GameButton buttonClose;

		public GameButton backgroundCloseButton;

		public GameButton buttonEquip;

		public GameButton togglePinTrinket;

		public GameObject pinnedTrinketParent;

		public GameButton buttonRecycle;

		public GameButton buttonNext;

		public GameButton buttonPrevious;

		public RectTransform popupRect;

		public RectTransform destroyRewardParent;

		public Text destroyRewardText;

		public MenuShowCurrency menuShowCurrency;

		public UiState stateToReturn;

		public bool isReturningBack;
	}
}
