using System;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelTrinketRecycle : AahMonoBehaviour
	{
		public DropPosition GetDropPosition(CurrencyType currencyType, Transform invTransform)
		{
			DropPosition dropPosition = new DropPosition
			{
				invPos = invTransform.position,
				targetToScaleOnReach = invTransform
			};
			dropPosition.startPos = ((currencyType != CurrencyType.GEM) ? this.rewardTextTokens.transform.position : this.rewardTextGems.transform.position);
			dropPosition.startPos = ((currencyType != CurrencyType.GEM) ? this.rewardTextTokens.transform.position : this.rewardTextGems.transform.position) - Vector3.up * 0.1f;
			return dropPosition;
		}

		public void InitStrings()
		{
			this.orLabel.text = LM.Get("UI_OR");
		}

		public Text header;

		public Text desc;

		public Text orLabel;

		public GameButton backgroundButton;

		public ButtonUpgradeAnim buttonGems;

		public ButtonUpgradeAnim buttonTokens;

		public Text rewardTextGems;

		public Text rewardTextTokens;

		public Image trinketImage;

		public Sprite disenchantSprite;

		public Sprite destroySprite;

		public MenuShowCurrency tokens;

		public MenuShowCurrency gems;

		public MenuShowCurrency scraps;

		public RectTransform popupRect;

		public GameButton closeButton;

		[NonSerialized]
		public UiState previousState;
	}
}
