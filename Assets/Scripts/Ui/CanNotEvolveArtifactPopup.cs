using System;
using Simulation.ArtifactSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class CanNotEvolveArtifactPopup : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public void InitStrings()
		{
			this.header.text = "UI_CAN_NOT_EVOLVE_HEADER".Loc();
			this.messageTop.text = "UI_CAN_NOT_EVOLVE_TOP_MESSAGE".Loc();
		}

		public override void Init()
		{
			this.InitStrings();
			this.okButton.onClick = (this.backgroundButton.onClick = new GameButton.VoidFunc(this.OnClosePopupButtonClicked));
		}

		public void OnPopupAppeared(ArtifactsManager artifactsManager)
		{
			if (artifactsManager.HasRechedTotalArtifactLevelMaxMilestone())
			{
				this.maxedIcon.enabled = true;
				this.messageMidleParent.SetActive(false);
				this.messageBot.text = "UI_CAN_NOT_EVOLVE_BOT_MESSAGE_MAX".Loc();
				this.popupParent.SetSizeDeltaY(this.maxedPopupHeight);
			}
			else
			{
				this.maxedIcon.enabled = false;
				this.messageMidleParent.SetActive(true);
				this.messageMidle.text = "UI_CAN_NOT_EVOLVE_MID_MESSAGE".LocFormat(artifactsManager.GetTotalArtifactsLevelOfNextMilestone());
				this.messageBot.text = "UI_CAN_NOT_EVOLVE_BOT_MESSAGE".Loc();
				this.popupParent.SetSizeDeltaY(this.notMaxedPopupHeight);
			}
		}

		private void OnClosePopupButtonClicked()
		{
			this.manager.state = this.previousState;
			UiManager.AddUiSound(SoundArchieve.inst.uiPopupDisappear);
		}

		[NonSerialized]
		public UiManager manager;

		[NonSerialized]
		public UiState previousState;

		[SerializeField]
		private Text header;

		[SerializeField]
		private Text messageTop;

		[SerializeField]
		private Text messageMidle;

		[SerializeField]
		private Text messageBot;

		[SerializeField]
		private GameObject messageMidleParent;

		[SerializeField]
		private Image maxedIcon;

		[SerializeField]
		private GameButton okButton;

		[SerializeField]
		private GameButton backgroundButton;

		[SerializeField]
		private RectTransform popupParent;

		[SerializeField]
		private float maxedPopupHeight;

		[SerializeField]
		private float notMaxedPopupHeight;
	}
}
