using System;
using DG.Tweening;
using DynamicLoading;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class ArtifactOverhaulPopup : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public void InitStrings()
		{
			this.header.text = "UI_ARTIFACT_OVERHAUL_POPUP_HEADER".Loc().ToUpper(LM.culture);
		}

		public override void Init()
		{
			this.InitStrings();
			this.okButton.onClick = new GameButton.VoidFunc(this.OnClosePopupButtonClicked);
			this.imageBot.rectTransform.SetScale(0f);
			this.imageTop.rectTransform.SetScale(0f);
		}

		public void OnPopupAppeared()
		{
			this.imagesBundle.LoadObjectAsync(new Action<AssetsContainer>(this.OnBundleImagesLoaded));
			this.closeOnClick = false;
			this.message1.text = "UI_ARTIFACT_OVERHAUL_POPUP_TEXT_1".Loc();
			this.message2.text = "UI_ARTIFACT_OVERHAUL_POPUP_TEXT_3".Loc();
			this.message3.text = "UI_ARTIFACT_OVERHAUL_POPUP_TEXT_2".Loc();
			this.okButton.text.text = "UI_OHNO".Loc();
			this.popupParent.SetScale(1f);
		}

		public void OnPopupClosed()
		{
			this.imagesBundle.Unload();
			this.assetsContainer = null;
			this.imageBot.sprite = null;
			this.imageBot.rectTransform.SetScale(0f);
			this.imageTop.sprite = null;
			this.imageTop.rectTransform.SetScale(0f);
		}

		private void OnBundleImagesLoaded(AssetsContainer container)
		{
			this.assetsContainer = container;
			if (this.closeOnClick)
			{
				this.imageBot.sprite = this.assetsContainer.sprites[1];
				this.imageTop.sprite = this.assetsContainer.sprites[2];
				this.imageBot.SetNativeSize();
				this.imageTop.SetNativeSize();
				this.imageBot.rectTransform.SetAnchorPosY(this.posImageBot2);
				this.imageTop.rectTransform.SetAnchorPosY(this.posImageTop2);
			}
			else
			{
				this.imageBot.sprite = this.assetsContainer.sprites[2];
				this.imageTop.sprite = this.assetsContainer.sprites[0];
				this.imageBot.SetNativeSize();
				this.imageTop.SetNativeSize();
				this.imageBot.rectTransform.SetAnchorPosY(this.posImageBot1);
				this.imageTop.rectTransform.SetAnchorPosY(this.posImageTop1);
			}
			this.imageBot.rectTransform.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
			this.imageTop.rectTransform.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
		}

		private void OnClosePopupButtonClicked()
		{
			if (this.closeOnClick)
			{
				this.manager.panelArtifactsCraft.skipArtifactShow = true;
				this.manager.state = UiState.ARTIFACTS_CRAFT;
				this.manager.panelArtifactsCraft.uistateToReturn = UiState.HUB_ARTIFACTS;
				this.manager.SetCommand(new UiCommandArtifactOverhaul());
				UiManager.AddUiSound(SoundArchieve.inst.uiPopupDisappear);
				UiManager.sounds.Add(new SoundEventUiVoiceSimple(SoundArchieve.inst.voAlchemistOverhaul, "ALCHEMIST", 1f));
				this.popupParent.DOScale(0f, 0.3f).SetEase(Ease.InBack).OnComplete(delegate
				{
					this.OnPopupClosed();
				});
			}
			else
			{
				if (this.assetsContainer != null)
				{
					this.imageBot.sprite = this.assetsContainer.sprites[1];
					this.imageTop.sprite = this.assetsContainer.sprites[2];
					this.imageBot.SetNativeSize();
					this.imageTop.SetNativeSize();
					this.imageBot.rectTransform.SetAnchorPosY(this.posImageBot2);
					this.imageTop.rectTransform.SetAnchorPosY(this.posImageTop2);
					this.imageBot.rectTransform.SetScale(0f);
					this.imageTop.rectTransform.SetScale(0f);
					this.imageBot.rectTransform.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
					this.imageTop.rectTransform.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
				}
				this.closeOnClick = true;
				this.message1.text = "UI_ARTIFACT_OVERHAUL_POPUP_TEXT_4".Loc();
				this.message2.text = "UI_ARTIFACT_OVERHAUL_POPUP_TEXT_6".Loc();
				this.message3.text = "UI_ARTIFACT_OVERHAUL_POPUP_TEXT_5".Loc();
				this.okButton.text.text = "UI_GO".Loc();
				UiManager.AddUiSound(SoundArchieve.inst.uiTabSwitch);
			}
		}

		[NonSerialized]
		public UiManager manager;

		[SerializeField]
		private Text header;

		[SerializeField]
		private Text message1;

		[SerializeField]
		private Text message2;

		[SerializeField]
		private Text message3;

		[SerializeField]
		private Image imageBot;

		[SerializeField]
		private Image imageTop;

		[SerializeField]
		private BAssetsContainer imagesBundle;

		[SerializeField]
		private GameButton okButton;

		[SerializeField]
		private float posImageBot1;

		[SerializeField]
		private float posImageTop1;

		[SerializeField]
		private float posImageBot2;

		[SerializeField]
		private float posImageTop2;

		[SerializeField]
		private RectTransform popupParent;

		private bool closeOnClick;

		private AssetsContainer assetsContainer;
	}
}
