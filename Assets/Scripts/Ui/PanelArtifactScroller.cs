using System;
using System.Collections.Generic;
using DG.Tweening;
using Simulation;
using Simulation.ArtifactSystem;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Ui
{
	public class PanelArtifactScroller : AahMonoBehaviour
	{
		public RectTransform rectTransform
		{
			get
			{
				RectTransform result;
				if ((result = this.m_rectTransform) == null)
				{
					result = (this.m_rectTransform = base.GetComponent<RectTransform>());
				}
				return result;
			}
		}

		public override void Register()
		{
			base.AddToInits();
			this.scrollRect.onValueChanged.AddListener(new UnityAction<Vector2>(this.OnScrollValueChanged));
		}

		public override void Init()
		{
			this.panelSelectedArtifact.buttonFiveX.onClick = new GameButton.VoidFunc(this.Button_OnUpgradeFiveClicked);
			this.filtersParentDefaultPos = this.filtersParent.anchoredPosition.y;
			this.filtersParentParentDefaultPos = this.filtersParentParent.anchoredPosition.y;
			this.boilerDefaultPos = this.boilerTransform.anchoredPosition.y;
			this.boilerBackgroundDefaultSize = this.boilerBackground.rectTransform.sizeDelta.y;
			this.buttonClaudronGlow.SetAlpha(0f);
		}

		public void OnScreenAppear()
		{
			for (int i = this.buttonArtifacts.Count - 1; i >= 0; i--)
			{
				this.buttonArtifacts[i].resetEvolveAnim = true;
			}
		}

		private void Button_OnUpgradeFiveClicked()
		{
			this.multipleBuyIndex++;
			if (this.multipleBuyIndex >= this.multipleBuySteps.Length)
			{
				this.multipleBuyIndex = 0;
			}
			this.uiManager.SetCommand(new CommandToggleArtifactUpgradeMultiplier
			{
				value = this.multipleBuyIndex
			});
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiToggleOn, 1f));
		}

		public void OnScrollValueChanged(Vector2 arg0)
		{
			if (this.isFilterShowing)
			{
				Vector3 a = this.scrollRect.viewport.RectViewportToWorldPosition(new Vector2(0.5f, 1f));
				if ((a - this.filtersParentParent.position).y <= -0.18f)
				{
					this.filtersParent.SetPosY(a.y - -0.1f);
				}
				else
				{
					this.filtersParent.SetAnchorPosY(-230f);
				}
			}
		}

		public void InitStrings()
		{
			this.buttonUnlockSlot.buttonUpgrade.textDown.text = LM.Get("UI_ARTIFACTS_UNLOCK_SLOT");
			this.textAllArtifactsCrafted.text = LM.Get("UI_ALL_ARTIFACTS_CRAFTED");
			this.craftButton.textDown.text = LM.Get("UI_ARTIFACTS_CRAFT");
			this.textTALLabel.text = LM.Get("UI_ARTIFACTS_TOTAL_ARTIFACTS_LEVEL");
			this.buttonTabMythical.text.text = LM.Get("UI_ARTIFACTS_MYTHICAL");
			this.buttonTabRegular.text.text = LM.Get("UI_ARTIFACTS_REGULAR");
			this.filterInfoText.text = LM.Get("ARTIFACT_SORT_BY");
		}

		public void CreateArtifactButtons(int count)
		{
			Utility.FillUiElementList<ButtonArtifact>(this.buttonArtifactsPrefab, this.artifactsParent, count, this.buttonArtifacts);
		}

		public void SetButtonEvents()
		{
			int num = 0;
			foreach (ButtonArtifact buttonArtifact in this.buttonArtifacts)
			{
				int ind = num;
				buttonArtifact.gameButton.onClick = delegate()
				{
					this.Button_OnArtifactSelected(ind);
				};
				num++;
			}
		}

		private void Button_OnArtifactSelected(int index)
		{
			UiManager.stateJustChanged = true;
			if (index == this.selectedArtifactIndex)
			{
				if (this.isLookingAtMythical)
				{
					this.buttonArtifacts[index].artifactStone.rectTransform.DOScale(1f, 0.1f);
				}
				this.selectedArtifactIndex = -1;
			}
			else
			{
				if (this.isLookingAtMythical)
				{
					if (this.selectedArtifactIndex != -1)
					{
						this.buttonArtifacts[this.selectedArtifactIndex].artifactStone.rectTransform.SetScale(1f);
					}
					this.buttonArtifacts[index].artifactStone.rectTransform.DOScale(1.2f, 0.1f);
				}
				this.selectedArtifactIndex = index;
				if (this.isLookingAtMythical)
				{
					this.OnMythicalArtifactSelected(this.selectedArtifactIndex, this.uiManager.sim.artifactsManager.MythicalArtifacts[this.selectedArtifactIndex], this.uiManager.sim.artifactsManager.NumArtifactSlotsMythical);
				}
			}
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiArtifactSlotSelect, 1f));
		}

		public void OnMythicalArtifactSelected(int selectedIndex, Simulation.Artifact artifact, int numAvailableSlots)
		{
			UpgradeSift jumpType = this.multipleBuySteps[this.multipleBuyIndex];
			MultiJumpResult mythicalArtifactEffectiveLevelJump = this.uiManager.GetMythicalArtifactEffectiveLevelJump(artifact, jumpType);
			float y = this.CallculateButtonPosition(selectedIndex).y;
			if (this.panelSelectedArtifact.SetArtifactInfo(artifact, y - 100f, mythicalArtifactEffectiveLevelJump.jumpCount, this.artifactSelectTransitionCurve))
			{
				this.CalculatePositions(true);
			}
			else if (this.panelSelectedArtifact.hasSizeChanged)
			{
				this.SetSelected(selectedIndex, this.panelSelectedArtifact.currentTargetHeight + 50f);
				this.buttonUnlockSlot.gameObject.SetActive(false);
				this.CalculateContentSize(this.uiManager.sim.artifactsManager.NumArtifactSlotsMythical);
				float buttonTopPositionRelativeToViewport = this.GetButtonTopPositionRelativeToViewport(selectedIndex);
				if (buttonTopPositionRelativeToViewport < 0f)
				{
					float f = buttonTopPositionRelativeToViewport - this.buttonArtifacts[selectedIndex].rectTransform.sizeDelta.y - 200f - this.panelSelectedArtifact.currentTargetHeight;
					if (Mathf.Abs(f) > this.scrollRect.viewport.rect.height)
					{
						this.SetScrollPosition(this.scrollRect.content.anchoredPosition.y + Mathf.Abs(f) - this.scrollRect.viewport.rect.height, true);
					}
				}
			}
		}

		public void CalculatePositions(bool animate = false)
		{
			int count = this.buttonArtifacts.Count;
			for (int i = 0; i < count; i++)
			{
				ButtonArtifact buttonArtifact = this.buttonArtifacts[i];
				if (animate)
				{
					buttonArtifact.rectTransform.DOAnchorPos(this.CallculateButtonPosition(i), 0.2f, false).SetEase(Ease.OutCirc);
				}
				else
				{
					buttonArtifact.rectTransform.anchoredPosition = this.CallculateButtonPosition(i);
				}
			}
		}

		public void ResetArtifactSelected()
		{
			if (!this.isAnimatingPop)
			{
				this.buttonArtifacts[this.selectedArtifactIndex].artifactStone.rectTransform.SetScale(1f);
			}
			this.selectedArtifactIndex = -1;
			this.CalculatePositions(false);
			this.panelSelectedArtifact.parentCanvas.enabled = false;
		}

		public void SetSelected(int index, float gap)
		{
			if (index == -1)
			{
				this.CalculatePositions(true);
				if (this.panelSelectedArtifact.parentCanvas.enabled && (this.panelSelectedArtifact.sequence == null || !this.panelSelectedArtifact.sequence.IsPlaying()))
				{
					if (this.panelSelectedArtifact.sequence != null && this.panelSelectedArtifact.sequence.IsPlaying())
					{
						this.panelSelectedArtifact.repositioning = false;
						this.panelSelectedArtifact.sequence.Kill(false);
					}
					this.panelSelectedArtifact.sequence = DOTween.Sequence().Append(this.panelSelectedArtifact.rectTransform.DOScaleY(0f, 0.2f).SetEase(Ease.InCirc)).AppendCallback(delegate
					{
						this.panelSelectedArtifact.parentCanvas.enabled = false;
						this.panelSelectedArtifact.rectTransform.SetSizeDeltaY(0f);
					}).Insert(0f, this.panelSelectedArtifact.parentCanvasGroup.DOFade(0f, 0.1f).SetEase(Ease.OutCirc)).Play<Sequence>();
				}
				return;
			}
			int num = Mathf.FloorToInt((float)index / 5f) + 1;
			int num2 = num * 5;
			int count = this.buttonArtifacts.Count;
			Vector2 b = new Vector2(0f, gap);
			for (int i = 0; i < count; i++)
			{
				ButtonArtifact buttonArtifact = this.buttonArtifacts[i];
				Vector2 vector = this.CallculateButtonPosition(i);
				if (i >= num2)
				{
					buttonArtifact.rectTransform.DOAnchorPos(vector - b, 0.2f, false).SetEase(this.artifactSelectTransitionCurve);
				}
				else
				{
					buttonArtifact.rectTransform.anchoredPosition = vector;
				}
			}
			if (this.GetButtonTopPositionRelativeToViewport(index) > 0f)
			{
				this.FocusOnArtifact(index, true);
			}
		}

		public float GetButtonTopPositionRelativeToViewport(int index)
		{
			return this.CallculateButtonPosition(index).y + this.buttonArtifacts[index].rectTransform.sizeDelta.y + this.artifactsParent.anchoredPosition.y + this.artifactsParent.sizeDelta.y * 0.5f + this.scrollRect.content.anchoredPosition.y;
		}

		public void FocusOnArtifactIfNecessary(int index, bool animate)
		{
			this.buttonArtifacts[index].rectTransform.GetWorldCorners(this.focusTargetCorners);
			this.scrollRect.viewport.GetWorldCorners(this.viewportCorners);
			if (this.focusTargetCorners[0].y < this.viewportCorners[0].y)
			{
				this.FocusOnArtifact(index, animate);
			}
		}

		public void FocusOnArtifact(int index, bool animate)
		{
			float buttonTopPositionRelativeToViewport = this.GetButtonTopPositionRelativeToViewport(index);
			float b = this.scrollRect.content.sizeDelta.y - this.scrollRect.viewport.rect.height;
			float minFloat = GameMath.GetMinFloat(this.scrollRect.content.anchoredPosition.y - buttonTopPositionRelativeToViewport, b);
			if (!animate || Math.Abs(this.scrollRect.velocity.y) < 10f)
			{
				this.SetScrollPosition(minFloat, animate);
			}
		}

		private void CancelScrollAnimIfNecessary()
		{
			if (this.animScrollTweener != null && this.animScrollTweener.IsPlaying())
			{
				this.animScrollTweener.Kill(false);
			}
		}

		public float GetArtifactFocusPosition(int index)
		{
			float b = this.scrollRect.content.sizeDelta.y - this.scrollRect.viewport.rect.height;
			return GameMath.GetMinFloat(-(this.CallculateButtonPosition(index).y - 300f), b);
		}

		public void SetScrollPosition(float pos, bool animate)
		{
			this.CancelScrollAnimIfNecessary();
			if (animate)
			{
				this.animScrollTweener = this.scrollRect.content.DOAnchorPosY(pos, 2000f, false).SetSpeedBased<Tweener>();
			}
			else
			{
				this.scrollRect.content.SetAnchorPosY(pos);
			}
		}

		public Vector2 CallculateButtonPosition(int index)
		{
			int num = index % 5;
			int num2 = index / 5;
			return new Vector2(150f * (float)num - 300f, -220f * (float)num2 - 100f);
		}

		public void CalculateContentSize(int count)
		{
			if (!this.isLookingAtMythical && count < 40 && count % 5 == 0)
			{
				count++;
			}
			int num = GameMath.CeilToInt((float)count / 5f);
			float num2 = 220f * (float)num + 100f + 460f;
			if (this.isLookingAtMythical)
			{
				num2 += 560f;
			}
			this.scrollRect.content.SetSizeDeltaY(num2);
		}

		public void SetRerollButton(string costString, bool interactable)
		{
			if (UiManager.stateJustChanged)
			{
				this.panelSelectedArtifact.buttonReroll.textUp.text = costString;
			}
			this.panelSelectedArtifact.buttonReroll.gameButton.interactable = interactable;
		}

		public void SetUpgradeButton(string costString, bool interactable)
		{
			if (UiManager.stateJustChanged)
			{
				this.panelSelectedArtifact.buttonReroll.textUp.text = costString;
			}
			this.panelSelectedArtifact.buttonReroll.gameButton.interactable = interactable;
			this.panelSelectedArtifact.buttonReroll.openWarningPopup = !interactable;
		}

		public void SetCraftButton(string costString, bool canCraft, bool canAffordCraft)
		{
			if (UiManager.stateJustChanged)
			{
				this.craftButton.textUp.text = costString;
			}
			this.craftButton.openWarningPopup = !canCraft;
			this.craftButton.spriteUpgrade = ((!canCraft) ? this.craftButton.spriteNotInteractable : this.craftButton.spriteLevelUp);
			this.craftButton.textUpCantAffordColorChangeForced = !canAffordCraft;
			this.craftButton.textDownCantAffordColorChangeForced = !canCraft;
			this.craftButton.gameButton.fakeDisabled = !canCraft;
		}

		public Vector2 GetArtifactPosition(int index)
		{
			return this.buttonArtifacts[index].artifactStone.transform.position;
		}

		public RectTransform GetSelectedArtifactStone(int index)
		{
			return this.buttonArtifacts[index].artifactStone.rectTransform;
		}

		public Vector2 GetSelectedArtifactPosition()
		{
			return this.buttonArtifacts[this.selectedArtifactIndex].artifactStone.transform.position;
		}

		public RectTransform GetSelectedArtifactWidget()
		{
			return this.buttonArtifacts[this.selectedArtifactIndex].rectTransform;
		}

		public RectTransform GetSelectedArtifactStone()
		{
			return this.buttonArtifacts[this.selectedArtifactIndex].artifactStone.rectTransform;
		}

		public Vector2 GetSelectedArtifactAnchoredPosition()
		{
			return this.buttonArtifacts[this.selectedArtifactIndex].rectTransform.anchoredPosition;
		}

		public ButtonArtifact GetSelectedArtifactButton()
		{
			return this.buttonArtifacts[this.selectedArtifactIndex];
		}

		public void SetInfoTraySize(bool isTalEnabled)
		{
			this.textTALLabel.rectTransform.SetAnchorPosY((float)((!isTalEnabled) ? -50 : -35));
			this.textNextTalLabel.gameObject.SetActive(isTalEnabled);
		}

		public void StartQpAnimation(int v)
		{
			if (this.isAnimatingQp)
			{
				return;
			}
			this.isAnimatingQp = true;
			int fontSize = this.textTALAmount.fontSize;
			this.textTALAmount.text = this.qpToShow.ToString();
			Sequence sequence = DOTween.Sequence();
			if (v - this.qpToShow == 1)
			{
				sequence.Append(this.textTALAmount.DOSize(fontSize + 15, 0.2f)).AppendCallback(delegate
				{
					this.qpToShow = v;
				}).AppendInterval(0.2f).Append(this.textTALAmount.DOSize(fontSize, 0.2f));
			}
			else
			{
				sequence.Append(DOTween.To(() => this.qpToShow, delegate(int x)
				{
					this.qpToShow = x;
				}, v, 1f)).Append(this.textTALAmount.DOSize(fontSize + 15, 0.4f)).AppendInterval(0.3f).Append(this.textTALAmount.DOSize(fontSize, 0.2f));
			}
			sequence.OnComplete(delegate
			{
				this.isAnimatingQp = false;
			}).Play<Sequence>();
		}

		public void ToggleFilterOptions()
		{
			this.isFilterShowing = !this.isFilterShowing;
			this.SetFilterOptions();
		}

		public void SetArtifactsParentPosition(bool isMythical)
		{
			this.artifactsParent.SetAnchorPosY(-755f);
		}

		private void SetFilterOptions()
		{
			if (this.isFilterShowing)
			{
				this.filtersParent.DOScaleY(1f, 0.3f).SetEase(Ease.OutBack);
				this.artifactsParent.DOAnchorPosY(-910f, 0.3f, false).SetEase(Ease.OutBack);
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuUp, 1f));
			}
			else
			{
				this.filtersParent.DOScaleY(0f, 0.3f).SetEase(Ease.OutCubic);
				this.artifactsParent.DOAnchorPosY(-810f, 0.3f, false).SetEase(Ease.OutCubic);
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiMenuDown, 1f));
			}
		}

		public void ShowFilterOptions()
		{
			if (this.isFilterShowing)
			{
				return;
			}
			if (this.filterShowAnimation != null && this.filterShowAnimation.isPlaying)
			{
				this.filterShowAnimation.Complete(true);
			}
			this.isFilterShowing = true;
			this.filtersParent.gameObject.SetActive(true);
			this.filterShowAnimation = DOTween.Sequence();
			this.filterShowAnimation.Append(this.filtersParent.DOScaleY(1f, 0.2f).SetEase(Ease.OutBack)).Join(this.artifactsParent.DOAnchorPosY(-800f, 0.2f, false).SetEase(Ease.OutBack)).Play<Sequence>();
		}

		public void HideFilterOptions()
		{
			if (!this.isFilterShowing)
			{
				return;
			}
			if (this.filterShowAnimation != null && this.filterShowAnimation.isPlaying)
			{
				this.filterShowAnimation.Complete(true);
			}
			this.isFilterShowing = false;
			this.filterShowAnimation = DOTween.Sequence();
			this.filterShowAnimation.Append(this.filtersParent.DOScaleY(0f, 0.2f).SetEase(Ease.OutCubic)).Join(this.artifactsParent.DOAnchorPosY(-700f, 0.2f, false).SetEase(Ease.OutCubic)).AppendCallback(delegate
			{
				this.filtersParent.gameObject.SetActive(false);
			}).Play<Sequence>();
		}

		public void SetScrollPositionOnArtifactIndex(int selectedArtifactIndex)
		{
			this.scrollRect.viewport.GetWorldCorners(this.viewportCorners);
			this.buttonArtifacts[selectedArtifactIndex].rectTransform.GetWorldCorners(this.focusTargetCorners);
			if (this.focusTargetCorners[0].y < this.viewportCorners[0].y)
			{
				this.SetScrollPosition(this.GetArtifactFocusPosition(selectedArtifactIndex), false);
			}
		}

		public void PopLastSelectedArtifact()
		{
			this.popAnimationIndex = this.selectedArtifactIndex;
			this.isAnimatingPop = true;
			ButtonArtifact buttonArtifact = this.buttonArtifacts[this.selectedArtifactIndex];
			buttonArtifact.artifactStone.transform.SetScale(1.3f);
			buttonArtifact.artifactStone.transform.DOScale(1f, 0.6f).SetEase(Ease.OutBounce).SetId("ArtifactPop").OnComplete(delegate
			{
				this.isAnimatingPop = false;
				this.popAnimationIndex = -1;
			});
		}

		public void PrepareForArtifactsAppearAnimAfterConversion()
		{
			this.CancelScrollAnimIfNecessary();
			this.scrollRect.verticalNormalizedPosition = 0f;
			this.dontUpdateStoneScale = true;
			foreach (ButtonArtifact buttonArtifact in this.buttonArtifacts)
			{
				if (buttonArtifact.state == ButtonArtifact.State.FULL)
				{
					buttonArtifact.artifactStone.rectTransform.SetScale(0f);
					Material material = new Material(this.glowingMaterialArtifact);
					material.SetFloat("_EffectAmount", 1f);
					buttonArtifact.artifactStone.imageStone.material = material;
					buttonArtifact.artifactStone.imageIcon.material = material;
				}
			}
		}

		public void PlayArtifactsAppearAnimAfterConversion()
		{
			float num = 0f;
			this.uiManager.inputBlocker.SetActive(true);
			this.artifactAppearAnimAfterConversion = DOTween.Sequence();
			int count = this.uiManager.sim.artifactsManager.Artifacts.Count;
			if (count > 5)
			{
				UiManager.sounds.Add(new SoundEventUiLooped(SoundArchieve.inst.uiArtifactAppearConversionLoop, "artifactAppearConversion", 1f));
			}
			for (int i = count - 1; i >= 0; i--)
			{
				ButtonArtifact button = this.buttonArtifacts[i];
				this.artifactAppearAnimAfterConversion.Insert(num, button.artifactStone.rectTransform.DOScale(1f, 0.1f).SetEase(Ease.OutBack));
				this.artifactAppearAnimAfterConversion.Insert(num, button.artifactStone.imageStone.materialForRendering.DOFloat(0f, "_EffectAmount", 0.1f));
				this.artifactAppearAnimAfterConversion.InsertCallback(num + 0.0800000057f, delegate
				{
					button.appearParticlesSpine.AnimationState.SetAnimation(0, "animation", false);
				});
				if (this.artifactCount <= 5)
				{
					this.artifactAppearAnimAfterConversion.InsertCallback(num + 0.1f, delegate
					{
						UiManager.AddUiSound(SoundArchieve.inst.uiArtifactAppearConversionSingle);
					});
				}
				num += 0.05f;
			}
			if (this.artifactCount > 5)
			{
				this.artifactAppearAnimAfterConversion.AppendCallback(delegate
				{
					UiManager.sounds.Add(new SoundEventCancelBy("artifactAppearConversion"));
				});
			}
			this.artifactAppearAnimAfterConversion.AppendInterval(1f);
			this.artifactAppearAnimAfterConversion.AppendCallback(delegate
			{
				this.uiManager.inputBlocker.SetActive(false);
				this.dontUpdateStoneScale = false;
				foreach (ButtonArtifact buttonArtifact in this.buttonArtifacts)
				{
					if (buttonArtifact.state == ButtonArtifact.State.FULL)
					{
						buttonArtifact.artifactStone.imageStone.material = null;
						buttonArtifact.artifactStone.imageIcon.material = null;
					}
				}
			});
			this.artifactAppearAnimAfterConversion.Play<Sequence>();
			int num2 = GameMath.CeilToInt((float)this.uiManager.sim.artifactsManager.Artifacts.Count / 5f);
			this.scrollRect.DOVerticalNormalizedPos(1f, (float)(num2 - 1) * 0.05f * 5f, false);
		}

		public void ResetScreenAnim()
		{
			this.SetArtifactsParentPosition(this.isLookingAtMythical);
			this.filtersParent.SetAnchorPosY(this.filtersParentDefaultPos);
			this.filtersParentParent.SetAnchorPosY(this.filtersParentParentDefaultPos);
			this.boilerTransform.gameObject.SetActive(true);
			this.boilerBackground.rectTransform.SetSizeDeltaY(this.boilerBackgroundDefaultSize);
			(this.uiManager.tabBar.transform as RectTransform).SetAnchorPosY(-50f);
			this.scrollRect.GetComponent<Mask>().enabled = true;
		}

		public void PlayNewTALMilestoneReachedAnim()
		{
			this.buttonEffects.rectTransform.parent.DOScale(1.1f, 0.5f).SetEase(EaseFactory.Kick(Ease.InSine, Ease.InSine));
			DOTween.Sequence().AppendInterval(0.3f).AppendCallback(delegate
			{
				this.buttonEffectsAnim.AnimationState.SetAnimation(0, "animation", false);
				UiManager.AddUiSound(SoundArchieve.inst.uiNewTALMilestoneReached);
			}).Insert(0f, this.buttonClaudronGlow.DOFade(1f, 0.25f).SetEase(Ease.InOutQuad)).Insert(0.25f, this.buttonClaudronGlow.DOFade(0f, 0.25f).SetEase(Ease.InOutQuad)).Play<Sequence>();
		}

		public void PlayArtifactCraftAnim()
		{
			this.uiManager.inputBlocker.SetActive(true);
			this.craftButton.gameObject.SetActive(false);
			this.boilerTransform.gameObject.SetActive(false);
			UiManager.AddUiSound(SoundArchieve.inst.uiArtifactCraftTableFall);
			this.scrollRect.GetComponent<Mask>().enabled = false;
			float num = -165.4f - this.boilerDefaultPos - 800f;
			DOTween.Sequence().Insert(0f, this.boilerBackground.rectTransform.DOSizeDeltaY(2500f, 0.4f, false).SetEase(Ease.InOutQuart)).Insert(0f, this.artifactsParent.DOAnchorPosY(-755f + num, 0.4f, false).SetEase(Ease.InOutQuart)).Insert(0f, this.filtersParentParent.DOAnchorPosY(this.filtersParentParentDefaultPos + num, 0.4f, false).SetEase(Ease.InOutQuart)).Insert(0f, this.filtersParent.DOAnchorPosY(this.filtersParentDefaultPos + num, 0.4f, false).SetEase(Ease.InOutQuart)).Insert(0f, (this.uiManager.tabBar.transform as RectTransform).DOAnchorPosY(-650f, 0.4f, false).SetEase(Ease.InOutQuart)).InsertCallback(1.5f, delegate
			{
				if (this.selectedArtifactIndex != -1)
				{
					ButtonArtifact buttonArtifact = this.buttonArtifacts[this.selectedArtifactIndex];
					buttonArtifact.isSelected = false;
					buttonArtifact.SetDetails();
					this.ResetArtifactSelected();
				}
				this.SetSelected(-1, 0f);
				if (this.isLookingAtMythical || this.uiManager.sim.artifactsManager.Artifacts.Count == this.buttonArtifacts.Count)
				{
					this.buttonUnlockSlot.gameObject.SetActive(false);
				}
				else
				{
					ButtonArtifact buttonArtifact2 = this.buttonArtifacts[(!this.isLookingAtMythical) ? this.uiManager.sim.artifactsManager.Artifacts.Count : this.uiManager.sim.artifactsManager.NumArtifactSlotsMythical];
					this.buttonUnlockSlot.rectTransform.anchoredPosition = buttonArtifact2.rectTransform.anchoredPosition;
				}
				this.uiManager.inputBlocker.SetActive(false);
			}).Play<Sequence>();
		}

		private RectTransform m_rectTransform;

		[NonSerialized]
		public UiManager uiManager;

		public ButtonUpgradeAnim craftButton;

		public ButtonArtifactSlotUnlock buttonUnlockSlot;

		public ScrollRect scrollRect;

		public RectTransform artifactsParent;

		public RectTransform noMythicalParent;

		public GameButton buttonInfo;

		public GameButton buttonEffects;

		public SkeletonGraphic buttonEffectsAnim;

		public Image buttonClaudronGlow;

		public GameButton buttonFilter;

		public GameButton buttonTabRegular;

		public GameButton buttonTabMythical;

		public Image mythicalTabLockIcon;

		[FormerlySerializedAs("textQpAmount")]
		public Text textTALAmount;

		[FormerlySerializedAs("textQpLabel")]
		public Text textTALLabel;

		public Text textNextTalLabel;

		public Text textAllArtifactsCrafted;

		public Text textMythicalHint;

		public ButtonArtifact buttonArtifactsPrefab;

		public RectTransform tabButtonsParent;

		public RectTransform filtersParent;

		public RectTransform filtersParentParent;

		public GameButton filterSwitchButton;

		public RectTransform filterArrow;

		public RectTransform boilerParent;

		public RectTransform boilerTransform;

		public RectTransform infoTray;

		public Text filterInfoText;

		[NonSerialized]
		public List<ButtonArtifact> buttonArtifacts;

		public Material glowingMaterialArtifact;

		public Image boilerBackground;

		public PanelSelectedArtifact panelSelectedArtifact;

		public SkeletonGraphic alchemyTable;

		public int selectedArtifactIndex = -1;

		public Simulation.Artifact pinnedArtifact;

		public bool isLookingAtMythical;

		public bool unlockButtonWaitingForConfirm;

		public int artifactCount;

		public bool isAnimatingQp;

		private Sequence reOrderAnim;

		public int qpToShow;

		public AnimationCurve artifactSelectTransitionCurve;

		public AnimationCurve artifactCraftTableFallCurve;

		private const int artifactsCollumnCount = 5;

		private const float collumntGap = 150f;

		private const float rowGap = 220f;

		private const float startXOffset = 300f;

		private const float startYOffset = 100f;

		private Tweener animScrollTweener;

		private float filtersParentParentDefaultPos;

		private float filtersParentDefaultPos;

		[NonSerialized]
		public float boilerDefaultPos;

		private float boilerBackgroundDefaultSize;

		public const float ScrollPositionAnimDur = 0.1f;

		public const string ArtifactPopAnimId = "ArtifactPop";

		public bool dontAnimateSort;

		public bool isFilterShowing;

		private Sequence filterShowAnimation;

		public int mythicalArtifactLevelJump;

		public int multipleBuyIndex;

		public UpgradeSift[] multipleBuySteps = new UpgradeSift[]
		{
			UpgradeSift.One,
			UpgradeSift.Five,
			UpgradeSift.TenPerCent,
			UpgradeSift.TwentyFivePerCent,
			UpgradeSift.Max
		};

		private Vector3[] focusTargetCorners = new Vector3[4];

		private Vector3[] viewportCorners = new Vector3[4];

		public bool isAnimatingPop;

		public int popAnimationIndex = -1;

		public Sequence artifactAppearAnimAfterConversion;

		[NonSerialized]
		public bool dontUpdateStoneScale;
	}
}
