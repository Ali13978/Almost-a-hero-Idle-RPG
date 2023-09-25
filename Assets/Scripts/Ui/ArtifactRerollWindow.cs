using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class ArtifactRerollWindow : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
			base.AddToUpdates();
		}

		public override void AahUpdate(float dt)
		{
			if (this.isAnimating)
			{
				if (this.tweenSpeed > 1f)
				{
					this.tweenSpeed -= dt;
				}
				else
				{
					this.tweenSpeed = 1f;
				}
			}
			else
			{
				this.tweenSpeed = 1f;
			}
			if (this.currentSequence != null && this.currentSequence.IsPlaying())
			{
				this.currentSequence.timeScale = this.tweenSpeed;
			}
			if (this.waitTime != 3.40282347E+38f)
			{
				this.waitTime -= dt;
				if (this.waitTime <= 0f)
				{
					this.PlayArtifactSelectedAnim(this.artifactIndex);
					this.waitTime = float.MaxValue;
				}
			}
		}

		public override void Init()
		{
			this.InitWidgets();
			this.InitStrings();
			this.bgSkip.onClick = new GameButton.VoidFunc(this.Button_SkipAnim);
			this.currencyTable.Init();
			this.currencyTable.SetCurrency(CurrencyType.MYTHSTONE, string.Empty, true, GameMode.STANDARD, true);
		}

		public void InitWidgets()
		{
			this.artifactWidget1 = UnityEngine.Object.Instantiate<ArtifactWidget>(this.artifactWidgetPrefab, base.transform);
			this.artifactWidget1.rectTransform.anchoredPosition = new Vector2(0f, 240f);
			this.artifactWidget2 = UnityEngine.Object.Instantiate<ArtifactWidget>(this.artifactWidgetPrefab, base.transform);
			this.artifactWidget2.rectTransform.anchoredPosition = new Vector2(0f, -106f);
			this.artifactWidget1.button.onClick = delegate()
			{
				this.Button_OnClickSeletArtifact(0);
			};
			this.artifactWidget2.button.onClick = delegate()
			{
				this.Button_OnClickSeletArtifact(1);
			};
			this.SetSelectedArtifact(0);
			this.widget1OriginalPos = this.artifactWidget1.rectTransform.anchoredPosition;
			this.widget2OriginalPos = this.artifactWidget2.rectTransform.anchoredPosition;
			this.artifactSlotPosition = this.artifactWidget1.artifactSlot.GetComponent<RectTransform>().anchoredPosition;
		}

		private void Button_SkipAnim()
		{
			if (this.currentSequence != null && this.currentSequence.IsPlaying())
			{
				this.tweenSpeed = 3f;
			}
		}

		private void Button_OnClickSeletArtifact(int v)
		{
			if (this.isAnimating)
			{
				return;
			}
			this.SetSelectedArtifact(v);
		}

		public void SetSelectedArtifact(int index)
		{
			bool flag = index == this.selectedArtifact;
			this.selectedArtifact = index;
			ArtifactWidget artifactWidget;
			if (index == 0)
			{
				artifactWidget = this.artifactWidget1;
				this.artifactWidget1.selectionParticleParent.gameObject.SetActive(true);
				this.artifactWidget1.selectionOutline.gameObject.SetActive(true);
				this.artifactWidget2.selectionParticleParent.gameObject.SetActive(false);
				this.artifactWidget2.selectionOutline.gameObject.SetActive(false);
				this.artifactWidget1.transform.SetAsLastSibling();
			}
			else
			{
				if (index != 1)
				{
					throw new Exception(index.ToString());
				}
				artifactWidget = this.artifactWidget2;
				this.artifactWidget1.selectionParticleParent.gameObject.SetActive(false);
				this.artifactWidget1.selectionOutline.gameObject.SetActive(false);
				this.artifactWidget2.selectionParticleParent.gameObject.SetActive(true);
				this.artifactWidget2.selectionOutline.gameObject.SetActive(true);
				this.artifactWidget2.transform.SetAsLastSibling();
			}
			int num = 222333;
			if (!flag)
			{
				if (DOTween.IsTweening(index + num, false))
				{
					DOTween.Goto(index + num, 0f, true);
				}
				else
				{
					artifactWidget.selectionOutline.color = this.outlineColor;
					artifactWidget.transform.localScale = Vector3.one;
					Sequence sequence = DOTween.Sequence();
					sequence.Append(artifactWidget.transform.DOPunchScale(new Vector3(0.03f, -0.02f, 0f), 0.5f, 7, 1f)).Insert(0.05f, artifactWidget.selectionOutline.DOColor(Color.white, 0.3f).SetEase(EaseFactory.Kick(Ease.OutQuint, Ease.InQuint)));
					sequence.SetId(index + num);
					sequence.Play<Sequence>();
				}
			}
		}

		public void InitStrings()
		{
			this.selectWarningLabel.text = LM.Get("UI_ARTIFACTS_REROLL_SELECT");
			this.rerollButton.textDown.text = LM.Get("UI_ARTIFACTS_REROLL_AGAIN");
			this.rerollSelect.text.text = LM.Get("UI_SELECT");
		}

		public Tween HideButtons(float dur)
		{
			Sequence sequence = DOTween.Sequence();
			sequence.Append(this.rerollButton.rectTransform.DOScale(0f, dur)).Join(this.rerollSelect.rectTransform.DOScale(0f, dur));
			return sequence;
		}

		public Tween ShowButtons(float dur)
		{
			Sequence sequence = DOTween.Sequence();
			sequence.Append(this.rerollButton.rectTransform.DOScale(1f, dur).SetEase(Ease.OutBack)).Join(this.rerollSelect.rectTransform.DOScale(1f, dur).SetEase(Ease.OutBack));
			return sequence;
		}

		public void InitReroll()
		{
			this.isAnimating = true;
			this.updateDetails = true;
			this.artifactWidget1.artifactSlot.GetComponent<RectTransform>().anchoredPosition = this.artifactSlotPosition;
			this.artifactWidget2.artifactSlot.GetComponent<RectTransform>().anchoredPosition = this.artifactSlotPosition;
			this.artifactWidget1.canvasGroup.alpha = 0f;
			this.artifactWidget1.artifactSlot.alpha = 0f;
			this.artifactWidget2.canvasGroup.alpha = 0f;
			this.artifactWidget2.artifactSlot.alpha = 0f;
			this.selectWarningParent.alpha = 1f;
			this.artifactWidget1.artifactStone.rectTransform.position = this.uiManager.panelArtifactScroller.GetSelectedArtifactPosition();
			this.artifactWidget2.artifactStone.rectTransform.position = new Vector2(base.transform.position.x, base.transform.position.y);
			this.artifactWidget2.artifactStone.rectTransform.localScale = Vector3.zero;
			this.artifactWidget1.artifactStone.rectTransform.localScale = Vector3.one;
			this.rerollButton.rectTransform.localScale = Vector3.zero;
			this.rerollSelect.rectTransform.localScale = Vector3.zero;
			this.currencyCanvasGroup.alpha = 0f;
			Color color = this.background.color;
			color.a = 0f;
			this.background.color = color;
			this.selectedArtifactTr = this.uiManager.panelArtifactScroller.GetSelectedArtifactStone();
			this.selectedArtifactTr.gameObject.SetActive(false);
			this.currentSequence = DOTween.Sequence();
			this.currentSequence.Append(this.background.DOFade(1f, 0.4f)).Join(this.artifactWidget1.artifactStone.rectTransform.DOScale(1.5f, 0.4f).SetEase(EaseFactory.Kick(Ease.OutQuint, Ease.OutQuint))).Join(this.artifactWidget1.artifactStone.rectTransform.DOAnchorPos(ArtifactRerollWindow.artifactStoneSitPos, 0.4f, false)).Join(this.artifactWidget1.artifactStone.rectTransform.DORotate(new Vector3(0f, 0f, 15f), 0.4f, RotateMode.Fast).SetEase(EaseFactory.Kick(Ease.OutQuint, Ease.OutQuint))).Join(this.currencyCanvasGroup.DOFade(1f, 0.4f));
			this.currentSequence.Join(this.artifactWidget1.canvasGroup.DOFade(1f, 0.4f)).Join(this.artifactWidget1.artifactSlot.DOFade(1f, 0.4f));
			this.currentSequence.AppendInterval(0.1f);
			this.currentSequence.Append(this.artifactWidget2.artifactStone.rectTransform.DOScale(1.5f, 0.3f).SetEase(Ease.OutBack));
			this.currentSequence.AppendInterval(0.2f);
			this.currentSequence.Append(this.artifactWidget2.artifactStone.rectTransform.DOAnchorPos(ArtifactRerollWindow.artifactStoneSitPos, 0.4f, false)).Join(this.artifactWidget2.artifactStone.rectTransform.DORotate(new Vector3(0f, 0f, -15f), 0.4f, RotateMode.Fast).SetEase(EaseFactory.Kick(Ease.OutQuint, Ease.OutQuint))).Join(this.artifactWidget2.artifactStone.rectTransform.DOScale(1f, 0.4f)).Join(this.artifactWidget2.canvasGroup.DOFade(1f, 0.4f)).Join(this.artifactWidget2.artifactSlot.DOFade(1f, 0.4f));
			this.currentSequence.Append(this.ShowButtons(0.2f));
			this.currentSequence.AppendCallback(delegate
			{
				this.isAnimating = false;
				this.tweenSpeed = 1f;
			});
			this.currentSequence.Play<Sequence>();
		}

		public void RerollAgain()
		{
			this.isAnimating = true;
			this.currentSequence = DOTween.Sequence();
			ArtifactWidget selectedWidget = null;
			Vector2 originalPos = Vector2.zero;
			float num;
			if (this.selectedArtifact == 0)
			{
				selectedWidget = this.artifactWidget1;
				originalPos = this.widget1OriginalPos;
				num = 1f;
			}
			else
			{
				if (this.selectedArtifact != 1)
				{
					throw new Exception(this.selectedArtifact.ToString());
				}
				selectedWidget = this.artifactWidget2;
				originalPos = this.widget2OriginalPos;
				num = -1f;
			}
			this.currentSequence.Append(this.HideButtons(0.1f));
			this.currentSequence.Append(selectedWidget.rectTransform.DOAnchorPosX(1000f * num, 0.15f, false)).AppendCallback(delegate
			{
				selectedWidget.rectTransform.anchoredPosition = originalPos;
				selectedWidget.artifactStone.rectTransform.position = new Vector2(this.transform.position.x, this.transform.position.y);
				selectedWidget.artifactStone.rectTransform.SetAnchorPosY(ArtifactRerollWindow.artifactStoneSitPos.y);
				selectedWidget.artifactStone.rectTransform.localScale = Vector3.zero;
				selectedWidget.canvasGroup.alpha = 0f;
				selectedWidget.artifactSlot.alpha = 0f;
				this.updateDetails = true;
			});
			this.currentSequence.Append(selectedWidget.artifactStone.rectTransform.DOScale(1.5f, 0.4f).SetEase(Ease.OutBack));
			this.currentSequence.AppendInterval(0.2f);
			this.currentSequence.Append(selectedWidget.artifactStone.rectTransform.DOAnchorPos(ArtifactRerollWindow.artifactStoneSitPos, 0.3f, false)).Join(selectedWidget.artifactStone.rectTransform.DOScale(1f, 0.3f)).Join(selectedWidget.canvasGroup.DOFade(1f, 0.3f)).Join(selectedWidget.artifactSlot.DOFade(1f, 0.3f)).Join(selectedWidget.artifactStone.rectTransform.DORotate(new Vector3(0f, 0f, -15f), 0.3f, RotateMode.Fast).SetEase(EaseFactory.Kick(Ease.OutQuint, Ease.OutQuint)));
			this.currentSequence.Append(this.ShowButtons(0.2f));
			this.currentSequence.AppendCallback(delegate
			{
				this.isAnimating = false;
				this.tweenSpeed = 1f;
			});
			this.currentSequence.Play<Sequence>();
		}

		public void ArtifactSelected(int index)
		{
			this.isAnimating = true;
			this.artifactIndex = index;
			this.waitTime = 0.2f;
			this.uiManager.panelArtifactScroller.FocusOnArtifact(index, true);
		}

		private void PlayArtifactSelectedAnim(int index)
		{
			ArtifactWidget selectedWidget = null;
			if (this.selectedArtifact == 0)
			{
				selectedWidget = this.artifactWidget1;
			}
			else
			{
				if (this.selectedArtifact != 1)
				{
					throw new Exception(this.selectedArtifact.ToString());
				}
				selectedWidget = this.artifactWidget2;
			}
			this.artifactWidget1.canvasGroup.alpha = 0f;
			this.artifactWidget1.artifactSlot.alpha = 0f;
			this.artifactWidget2.canvasGroup.alpha = 0f;
			this.artifactWidget2.artifactSlot.alpha = 0f;
			this.artifactWidget1.artifactStone.rectTransform.localScale = Vector3.zero;
			this.artifactWidget2.artifactStone.rectTransform.localScale = Vector3.zero;
			selectedWidget.artifactStone.rectTransform.localScale = Vector3.one;
			this.selectWarningParent.alpha = 0f;
			this.rerollButton.rectTransform.localScale = Vector3.zero;
			this.rerollSelect.rectTransform.localScale = Vector3.zero;
			this.currencyCanvasGroup.alpha = 0f;
			selectedWidget.artifactSlot.alpha = 0f;
			this.selectedArtifactTr = this.uiManager.panelArtifactScroller.GetSelectedArtifactStone(index);
			this.selectedArtifactTr.gameObject.SetActive(false);
			ButtonArtifact buttonArtifact = this.uiManager.panelArtifactScroller.buttonArtifacts[index];
			this.currentSequence = DOTween.Sequence();
			this.currentSequence.Append(selectedWidget.artifactStone.transform.DOMove(new Vector2(base.transform.position.x, base.transform.position.y), 0.35f, false).SetEase(Ease.OutCubic)).Join(selectedWidget.artifactStone.transform.DOScale(1.5f, 0.35f).SetEase(Ease.OutCubic)).Join(this.background.DOFade(0f, 0.25f)).Append(selectedWidget.artifactStone.transform.DOMove(this.uiManager.panelArtifactScroller.GetArtifactPosition(index), 0.35f, false).SetEase(Ease.InCubic)).Join(selectedWidget.artifactStone.transform.DOScale(1f, 0.35f).SetEase(Ease.InCubic)).AppendCallback(delegate
			{
				selectedWidget.artifactSlot.alpha = 0f;
				this.selectedArtifactTr.gameObject.SetActive(true);
				UiManager.zeroScrollViewContentY = false;
				this.uiManager.state = this.uistateToReturn;
				this.uiManager.panelArtifactScroller.StartQpAnimation(this.uiManager.sim.artifactsManager.TotalArtifactsLevel);
			});
			this.currentSequence.AppendCallback(delegate
			{
				this.isAnimating = false;
			});
			this.currentSequence.Play<Sequence>();
		}

		public void DisableTargetArtifact()
		{
			this.selectedArtifactTr.gameObject.SetActive(false);
		}

		public CanvasGroup currencyCanvasGroup;

		public MenuShowCurrency currencyTable;

		public CanvasGroup selectWarningParent;

		public Image background;

		public Text selectWarningLabel;

		public ArtifactWidget artifactWidgetPrefab;

		[HideInInspector]
		public ArtifactWidget artifactWidget1;

		[HideInInspector]
		public ArtifactWidget artifactWidget2;

		public ArtifactAttributeLabel artifactAttributePrefab;

		public ButtonUpgradeAnim rerollButton;

		public GameButton rerollSelect;

		public int selectedArtifact;

		public bool rrr;

		public bool rrra;

		private static readonly Vector2 artifactStoneSitPos = new Vector2(0f, -136f);

		private Vector2 widget1OriginalPos;

		private Vector2 widget2OriginalPos;

		public UiManager uiManager;

		public bool updateDetails;

		private RectTransform selectedArtifactTr;

		public bool isAnimating;

		public GameButton bgSkip;

		public Color outlineColor;

		public UiState uistateToReturn;

		private Sequence currentSequence;

		private float tweenSpeed = 1f;

		private Vector2 artifactSlotPosition;

		public bool confirmed;

		public int targetIndex;

		private float waitTime = float.MaxValue;

		private int artifactIndex;
	}
}
