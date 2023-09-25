using System;
using DG.Tweening;
using Simulation.ArtifactSystem;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class ArtifactEvolveWindow : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
			base.AddToUpdates();
		}

		public override void Init()
		{
			this.InitStrings();
			this.buttonOk.onClick = new GameButton.VoidFunc(this.OnOkButtonClickedHandler);
			this.evolveAnim.Initialize(true);
		}

		public override void AahUpdate(float dt)
		{
			if (this.targetRarity == this.artifact.Rarity)
			{
				this.newStatDescription.enabled = true;
				int key = this.artifact.UniqueEffectsIds[this.artifact.UniqueEffectsIds.Count - 1];
				this.newStatDescription.text = EffectsDatabase.Unique[key].Effect.GetDescriptionWithValue(1, this.manager.sim.GetUniversalBonusAll());
			}
			else
			{
				this.newStatDescription.enabled = false;
			}
		}

		public void InitStrings()
		{
			this.buttonOk.text.text = "UI_OKAY".Loc();
			this.header.text = "UI_ARTIFACT_EVOLVED".Loc();
			this.newStatLabel.text = "UI_NEW_STAT".Loc();
		}

		public void SetArtifact(Artifact artifact, UiManager manager)
		{
			this.manager = manager;
			this.targetRarity = artifact.Rarity + 1;
			this.artifactWidget.SetButton(ButtonArtifact.State.FULL, artifact.Level, artifact.Rarity);
			this.artifactWidget.artifactStone.imageIcon.sprite = manager.GetEffectSprite(artifact.CommonEffectId);
			UiManager.sounds.Add(new SoundEventUiVariedVoiceSimple(SoundArchieve.inst.voAlchemistEvolve[this.targetRarity].Clips, "ALCHEMIST", 1f));
			this.artifact = artifact;
			this.PlayAnim();
		}

		private void PlayAnim()
		{
			this.boneFollower.enabled = true;
			if (!this.evolveAnim.IsValid)
			{
				this.evolveAnim.Initialize(true);
			}
			this.evolveAnim.AnimationState.ClearTracks();
			this.evolveAnim.AnimationState.SetAnimation(0, "animation", false);
			this.artifactPanelCanvasGroup.alpha = 0f;
			this.artifactEvolvedRibbonParent.SetScale(0f);
			this.newStatLabel.rectTransform.SetScale(0f);
			this.buttonOk.transform.SetScale(0f);
			this.newStatDescription.rectTransform.SetScaleX(0f);
			this.glowingSpriteMaterial.SetFloat("_EffectAmount", 0f);
			this.artifactEvolvedRibbonParent.SetAnchorPosY(245.7f - (float)this.targetRarity * 5.2f);
			this.artifactPanelParent.SetAnchorPosY(-40f - (float)this.targetRarity * 5.2f);
			DOTween.Sequence().Append(this.background.DOFade(1f, 0.3f)).AppendInterval(0.9f).Append(this.glowingSpriteMaterial.DOFloat(1f, "_EffectAmount", 0.166666672f)).AppendInterval(0.4f).AppendCallback(delegate
			{
				this.artifactWidget.artifactStone.PlaySpineAnim(this.targetRarity);
			}).Append(this.glowingSpriteMaterial.DOFloat(0f, "_EffectAmount", 0.06666667f)).AppendInterval(1f).Append(this.artifactPanelCanvasGroup.DOFade(1f, 0.3f)).AppendInterval(0.5f).Append(this.artifactEvolvedRibbonParent.DOScale(1f, 0.3f).SetEase(Ease.OutBack)).AppendInterval(0.2f).Append(this.newStatLabel.rectTransform.DOScale(1f, 0.3f).SetEase(Ease.OutBack)).Insert(4.2f, this.newStatDescription.rectTransform.DOScaleX(1f, 0.3f).SetEase(Ease.OutBack)).Append(this.buttonOk.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack)).Play<Sequence>();
		}

		private void OnOkButtonClickedHandler()
		{
			this.manager.state = UiState.ARTIFACT_SELECTED_POPUP;
			UiManager.AddUiSound(SoundArchieve.inst.uiPopupDisappear);
		}

		[SerializeField]
		private Text header;

		[SerializeField]
		private Text newStatLabel;

		[SerializeField]
		private Text newStatDescription;

		[SerializeField]
		private ButtonArtifact artifactWidget;

		[SerializeField]
		private GameButton buttonOk;

		[SerializeField]
		private RectTransform artifactEvolvedRibbonParent;

		[SerializeField]
		private CanvasGroup artifactPanelCanvasGroup;

		[SerializeField]
		private Image background;

		[SerializeField]
		private SkeletonGraphic evolveAnim;

		[SerializeField]
		private BoneFollowerGraphic boneFollower;

		[SerializeField]
		private RectTransform artifactPanelParent;

		[SerializeField]
		private Material glowingSpriteMaterial;

		private UiManager manager;

		private Artifact artifact;

		private int targetRarity;
	}
}
