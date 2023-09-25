using System;
using DG.Tweening;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelArtifactsCraft : AahMonoBehaviour
	{
		public PanelArtifactsCraft.State state
		{
			get
			{
				return this._state;
			}
			set
			{
				switch (value)
				{
				case PanelArtifactsCraft.State.PrepareTransition:
					this.boilerSpine.Initialize(false);
					this.alchemist.Initialize(false);
					this.backgroundFollower.enabled = true;
					this.conversionLoopSoundTriggered = false;
					this.stoneScaler.SetAsFirstSibling();
					this.backgroundGlow.SetAlpha(0f);
					this.auxBackground.SetAlpha(0f);
					this.alchemistTableBackground.color = this.alchemistTableBackgroundColorDefault;
					this.newArtifactWarningParent.localScale = Vector3.zero;
					this.newArtifactWarningCanvasGroup.alpha = 1f;
					this.selectButton.gameObject.SetActive(true);
					this.imageBg.color = new Color(this.imageBg.color.r, this.imageBg.color.g, this.imageBg.color.b, 0f);
					this.craftedArtifactWidget.gameObject.SetActive(false);
					this.craftedArtifactWidget.canvasGroup.alpha = 0f;
					this.craftedArtifactWidget.artifactSlot.alpha = 0f;
					this.craftedArtifactWidget.artifactStone.canvasGroup.alpha = (float)((!this.skipArtifactShow) ? 1 : 0);
					this.selectButtonCanvasGroup.alpha = 0f;
					this.selectButton.interactable = false;
					this.boilerSpine.Skeleton.SetSkin((!this.uiManager.panelArtifactScroller.isLookingAtMythical) ? "table_01" : "table_02");
					this.boilerSpine.Skeleton.SetToSetupPose();
					this.alchemist.AnimationState.ClearTracks();
					this.boilerSpine.AnimationState.ClearTracks();
					if (this.skipArtifactShow)
					{
						this.canvasGroupBoiler.alpha = 0f;
						this.alchemist.color = new Color(1f, 1f, 1f, 1f);
						this.boilerSpine.AnimationState.SetAnimation(0, "animation_crafting_idle", true);
						this.alchemist.AnimationState.SetAnimation(0, "animation_idle", true);
						this.imageBoiler.SetAnchorPosY(-626f);
					}
					else
					{
						this.alchemist.AnimationState.SetAnimation(0, "animation_crafting_idle", true);
						this.canvasGroupBoiler.alpha = 1f;
						this.imageBoiler.SetPosY(this.uiManager.panelArtifactScroller.boilerTransform.position.y);
						this.boilerSpine.AnimationState.SetAnimation(0, "animation_fall_RAYCO", false);
						this.imageBoiler.DOAnchorPosY(-626f, 0.5f, false).SetEase(this.uiManager.panelArtifactScroller.artifactCraftTableFallCurve);
						this.alchemist.color = new Color(1f, 1f, 1f, 0f);
						this.imageBg.DOFade(1f, 0.4f);
					}
					this.stoneBoneFollewer.gameObject.SetActive(false);
					this.stoneBoneFollewer.enabled = true;
					this.glowingSpriteMaterial.SetFloat("_EffectAmount", 1f);
					this.craftedArtifactWidget.artifactNameLabel.rectTransform.SetScaleX(0f);
					this.craftedArtifactWidget.mythicalArtifactDesc.rectTransform.SetScaleX(0f);
					this.craftedArtifactWidget.regularArtifactDescParent.SetScale(0f);
					this.selectButton.rectTransform.SetAnchorPosY((float)((!this.isMythical) ? -389 : -508));
					foreach (BoneFollowerGraphic boneFollowerGraphic in this.conversionAnimArtifacts)
					{
						boneFollowerGraphic.enabled = true;
						boneFollowerGraphic.gameObject.SetActive(false);
					}
					this.period = ((!this.skipArtifactShow) ? 1.5f : 0.2f);
					break;
				case PanelArtifactsCraft.State.Begin:
					if (!this.skipArtifactShow)
					{
						this.boilerSpine.AnimationState.SetAnimation(0, "animation_crafting_idle", true);
					}
					this.period = 0.3f;
					break;
				case PanelArtifactsCraft.State.WaitAnim:
					if (this.skipArtifactShow)
					{
						UiManager.AddUiSound(SoundArchieve.inst.uiArtifactConversion);
					}
					else
					{
						UiManager.sounds.Add(new SoundEventUiDelayed(SoundArchieve.inst.uiArtifactCraft, 1.5f, 1f));
					}
					if (this.skipArtifactShow)
					{
						this.alchemist.AnimationState.SetAnimation(0, "animation", false);
						this.boilerSpine.AnimationState.SetAnimation(0, "animation", false);
						this.craftedArtifactWidget.gameObject.SetActive(false);
						this.craftedArtifactWidget.selectionParticleParent.gameObject.SetActive(false);
						this.period = 11f;
					}
					else
					{
						this.alchemist.AnimationState.SetAnimation(0, "animation_crafting", false);
						this.boilerSpine.AnimationState.SetAnimation(0, "animation_crafting", false);
						this.craftedArtifactWidget.gameObject.SetActive(true);
						this.stoneBoneFollewer.gameObject.SetActive(true);
						this.craftedArtifactWidget.selectionParticleParent.gameObject.SetActive(true);
						this.period = 4f;
					}
					DOTween.Sequence().AppendInterval((!this.skipArtifactShow) ? 2.23333335f : 3.4333334f).Append(this.alchemistTableBackground.DOColor(this.alchemistTableBackgroundColorTint, 0.166666672f)).AppendInterval((!this.skipArtifactShow) ? 1.86666667f : 3.73333335f).Append(this.alchemistTableBackground.DOColor(this.alchemistTableBackgroundColorDefault, (!this.skipArtifactShow) ? 0.266666681f : 0.166666672f)).Play<Sequence>();
					break;
				case PanelArtifactsCraft.State.ArtifactSit:
					this.stoneScaler.SetAsLastSibling();
					UiManager.sounds.Add(new SoundEventUiVariedVoiceSimple(SoundArchieve.inst.voAlchemistCraft, "ALCHEMIST", 1f));
					this.glowingSpriteMaterial.DOFloat(0f, "_EffectAmount", 0.2f);
					this.auxBackground.DOFade(1f, 0.3f);
					DOTween.Sequence().AppendInterval(1f).Append(this.craftedArtifactWidget.canvasGroup.DOFade(1f, 0.3f)).Insert(1f, this.craftedArtifactWidget.artifactSlot.DOFade(1f, 0.3f)).AppendInterval(0.2f).Append(this.newArtifactWarningParent.DOScale(1f, 0.3f).SetEase(Ease.OutBack)).Append(((!this.isMythical) ? this.craftedArtifactWidget.regularArtifactDescParent : this.craftedArtifactWidget.artifactNameLabel.rectTransform).DOScale(1f, 0.3f).SetEase(Ease.OutBack)).Insert(1.9f, this.craftedArtifactWidget.mythicalArtifactDesc.rectTransform.DOScaleX(1f, 0.3f).SetEase(Ease.OutBack)).Insert(2f, this.selectButtonCanvasGroup.DOFade(1f, 0.3f)).Play<Sequence>();
					this.period = 3f;
					break;
				case PanelArtifactsCraft.State.WaitForSelect:
					this.selectButton.interactable = true;
					break;
				case PanelArtifactsCraft.State.FadeOut:
					this.uiManager.panelArtifactScroller.ResetScreenAnim();
					if (this.skipArtifactShow)
					{
						UiManager.sounds.Add(new SoundEventCancelBy("artifactConversion"));
						this.uiManager.PrepareArtifactOverhaulAnimForFadeOut();
					}
					else
					{
						this.stoneBoxArtifactsScreen.gameObject.SetActive(false);
						int index = ((!this.isMythical) ? this.uiManager.sim.artifactsManager.Artifacts.Count : this.uiManager.sim.artifactsManager.MythicalArtifacts.Count) - 1;
						this.uiManager.panelArtifactScroller.FocusOnArtifact(index, false);
					}
					this.alchemist.AnimationState.SetAnimation(0, "animation_crafting_idle", false);
					this.boilerSpine.AnimationState.SetAnimation(0, "animation_crafting_idle", false);
					this.stoneBoneFollewer.enabled = false;
					this.selectButton.interactable = false;
					this.canvasGroupBoiler.alpha = 0f;
					this.alchemist.SetAlpha(0f);
					this.period = 0.5f;
					break;
				case PanelArtifactsCraft.State.WaitForTap:
					this.boilerSpine.AnimationState.SetAnimation(0, "animation_artifact_idle", true);
					break;
				}
				this._state = value;
				this.timer = 0f;
				this.animating = true;
			}
		}

		public override void Register()
		{
			base.AddToInits();
			base.AddToUpdates();
		}

		public override void Init()
		{
            this.gameButton.onDown = delegate ()
            {
                this.pressed = true;
            };
            this.selectButton.onClick = delegate ()
            {
                this.pressedSelect = true;
                UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiArtifactSlotSelect, 1f));
            };
            this.canvasGroupBoiler = this.imageBoiler.GetComponent<CanvasGroup>();
            this.boilerScale = this.imageBoiler.localScale.x;
            this.InitStrings();
            PanelArtifactsCraft.artifactStoneParent = this.craftedArtifactWidget.artifactHolderRect.position;
            this.boilerSpine.Initialize(true);
            this.alchemist.Initialize(true);
            this.newArtifactWarningCanvasGroup = this.newArtifactWarningParent.GetComponent<CanvasGroup>();
            foreach (BoneFollowerGraphic boneFollowerGraphic in this.conversionAnimArtifacts)
            {
                boneFollowerGraphic.enabled = false;
                boneFollowerGraphic.gameObject.SetActive(false);
            }
        }

		public void InitStrings()
		{
			this.selectButton.text.text = LM.Get("UI_SELECT");
			this.newArtifactWarning.text = LM.Get("UI_ARTIFACTS_NEW");
		}

		public override void AahUpdate(float dt)
		{
			switch (this.state)
			{
			case PanelArtifactsCraft.State.PrepareTransition:
				this.timer += dt;
				if (this.timer >= this.period)
				{
					this.state = PanelArtifactsCraft.State.Begin;
				}
				break;
			case PanelArtifactsCraft.State.Begin:
				if (this.animating)
				{
					this.timer += dt;
					if (this.timer < this.period && !this.pressed)
					{
						float a = Easing.Linear(this.timer, 0f, 1f, this.period);
						float num = Easing.SineEaseIn(this.timer, 0f, 1f, this.period);
						float num2 = Easing.CircEaseOut(this.timer, 0f, 1f, this.period);
						if (this.skipArtifactShow)
						{
							this.imageBg.SetAlpha(num2);
						}
						this.canvasGroupBoiler.alpha = ((!this.skipArtifactShow) ? 1f : num2);
						this.alchemist.SetAlpha(a);
						this.imageBoiler.localScale = Vector3.one * (1f + (this.boilerScale - 1f) * num);
					}
					else
					{
						this.imageBg.color = new Color(this.imageBg.color.r, this.imageBg.color.g, this.imageBg.color.b, 1f);
						this.canvasGroupBoiler.alpha = 1f;
						this.alchemist.SetAlpha(1f);
						this.imageBoiler.localScale = Vector3.one * this.boilerScale;
						this.animating = false;
					}
				}
				else
				{
					this.state = PanelArtifactsCraft.State.WaitAnim;
				}
				break;
			case PanelArtifactsCraft.State.WaitAnim:
				this.timer += dt;
				if (this.skipArtifactShow && !this.conversionAnimArtifacts[0].gameObject.activeSelf)
				{
					foreach (BoneFollowerGraphic boneFollowerGraphic in this.conversionAnimArtifacts)
					{
						boneFollowerGraphic.gameObject.SetActive(true);
					}
				}
				else if (!this.skipArtifactShow && !this.stoneBoneFollewer.gameObject.activeSelf)
				{
					this.stoneBoneFollewer.gameObject.SetActive(true);
				}
				if (this.timer >= this.period)
				{
					this.state = ((!this.skipArtifactShow) ? PanelArtifactsCraft.State.ArtifactSit : PanelArtifactsCraft.State.WaitForTap);
				}
				else if (!this.skipArtifactShow && this.timer >= 2.9333334f && this.timer <= 3.26666665f)
				{
					this.glowingSpriteMaterial.SetFloat("_EffectAmount", (3.26666665f - this.timer) / 0.3f);
				}
				else if (this.skipArtifactShow && this.timer > 8f)
				{
					if (!this.conversionLoopSoundTriggered)
					{
						this.conversionLoopSoundTriggered = true;
						UiManager.sounds.Add(new SoundEventUiLooped(SoundArchieve.inst.uiArtifactConversionLoop, "artifactConversion", 1f));
					}
					if (Input.GetMouseButtonDown(0) || UnityEngine.Input.touchCount > 0)
					{
						this.state = PanelArtifactsCraft.State.FadeOut;
					}
				}
				break;
			case PanelArtifactsCraft.State.ArtifactSit:
				if (this.animating)
				{
					this.timer += dt;
					if (this.timer > this.period || this.pressed)
					{
						this.animating = false;
					}
				}
				else
				{
					this.state = PanelArtifactsCraft.State.WaitForSelect;
				}
				break;
			case PanelArtifactsCraft.State.WaitForSelect:
				if (this.pressedSelect)
				{
					this.pressedSelect = false;
					this.stoneBoxArtifactsScreen = this.uiManager.GetTargetArtifactSit();
					this.uiManager.panelArtifactScroller.dontAnimateSort = true;
					UiManager.stateJustChanged = true;
					this.uiManager.UpdateArtifactScroller();
					this.uiManager.panelArtifactScroller.dontAnimateSort = false;
					this.state = PanelArtifactsCraft.State.FadeOut;
				}
				break;
			case PanelArtifactsCraft.State.FadeOut:
				if (this.animating)
				{
					this.selectButton.gameObject.SetActive(false);
					this.timer += dt;
					if (this.timer < this.period && !this.pressed)
					{
						float num3 = Easing.Linear(this.timer, 0f, 1f, this.period);
						float d = Easing.SineEaseOut(this.timer, 0f, 1f, this.period);
						this.imageBg.color = new Color(this.imageBg.color.r, this.imageBg.color.g, this.imageBg.color.b, 1f - num3);
						if (!this.skipArtifactShow)
						{
							this.auxBackground.SetAlpha(1f - num3);
							this.craftedArtifactWidget.canvasGroup.alpha = 1f - num3;
							this.craftedArtifactWidget.artifactSlot.alpha = 1f - num3;
							this.newArtifactWarningCanvasGroup.alpha = 1f - num3;
							this.craftedArtifactWidget.artifactStone.transform.position = PanelArtifactsCraft.artifactStoneParent + (this.stoneBoxArtifactsScreen.position - PanelArtifactsCraft.artifactStoneParent) * d;
						}
					}
					else
					{
						this.imageBg.color = new Color(this.imageBg.color.r, this.imageBg.color.g, this.imageBg.color.b, 0f);
						if (!this.skipArtifactShow)
						{
							this.craftedArtifactWidget.artifactSlot.alpha = 0f;
							this.craftedArtifactWidget.canvasGroup.alpha = 0f;
							this.craftedArtifactWidget.artifactSlot.alpha = 0f;
							this.newArtifactWarningCanvasGroup.alpha = 0f;
							this.auxBackground.SetAlpha(0f);
							this.craftedArtifactWidget.artifactStone.canvasGroup.alpha = 0f;
							this.craftedArtifactWidget.artifactStone.transform.position = PanelArtifactsCraft.artifactStoneParent;
							this.stoneBoxArtifact.gameObject.SetActive(true);
						}
						this.animating = false;
					}
				}
				else
				{
					this.selectButton.gameObject.SetActive(false);
					this.state = PanelArtifactsCraft.State.End;
				}
				break;
			case PanelArtifactsCraft.State.WaitForTap:
				if (Input.GetMouseButtonDown(0) || UnityEngine.Input.touchCount > 0)
				{
					this.state = PanelArtifactsCraft.State.FadeOut;
				}
				break;
			}
			if (this.pressed)
			{
				this.pressed = false;
			}
		}

		public UiState uistateToReturn;

		private PanelArtifactsCraft.State _state;

		public Image imageBg;

		public RectTransform imageBoiler;

		public SkeletonGraphic boilerSpine;

		public Image alchemistTableBackground;

		public Color alchemistTableBackgroundColorTint;

		public Color alchemistTableBackgroundColorDefault;

		public SkeletonGraphic alchemist;

		private CanvasGroup canvasGroupBoiler;

		public ArtifactWidget craftedArtifactWidget;

		public BoneFollowerGraphic stoneBoneFollewer;

		public Material glowingSpriteMaterial;

		public Image backgroundGlow;

		public BoneFollowerGraphic backgroundFollower;

		public Text newArtifactWarning;

		public RectTransform newArtifactWarningParent;

		private CanvasGroup newArtifactWarningCanvasGroup;

		public RectTransform stoneScaler;

		public Image auxBackground;

		public BoneFollowerGraphic[] conversionAnimArtifacts;

		public GameButton selectButton;

		public CanvasGroup selectButtonCanvasGroup;

		public GameButton gameButton;

		private bool pressed;

		private float timer;

		private float period;

		private bool animating;

		private float boilerScale;

		[NonSerialized]
		public bool updateDetails;

		[NonSerialized]
		public bool isMythical;

		[NonSerialized]
		public int craftedArtifactRarity;

		private bool pressedSelect;

		[NonSerialized]
		public ArtifactStone stoneBoxArtifact;

		[NonSerialized]
		public RectTransform stoneBoxArtifactsScreen;

		[NonSerialized]
		public UiManager uiManager;

		[NonSerialized]
		public bool skipArtifactShow;

		public ArtifactAttributeLabel artifactAttributePrefab;

		private bool conversionLoopSoundTriggered;

		private static Vector3 artifactStoneParent = new Vector3(146f, -186f);

		public enum State
		{
			PrepareTransition,
			Begin,
			WaitAnim,
			ArtifactSit,
			WaitForSelect,
			FadeOut,
			WaitForTap,
			End
		}
	}
}
