using System;
using DG.Tweening;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelSelectedArtifact : MonoBehaviour
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

		public bool SetArtifactInfo(Artifact artifact, float newPos, int levelJump, AnimationCurve expandAnimationCurve)
		{
			if (this.repositioning)
			{
				return false;
			}
			bool flag = newPos != this.rectTransform.anchoredPosition.y;
			if (this.parentCanvas.enabled && flag)
			{
				this.repositioning = true;
				if (this.sequence != null && this.sequence.IsPlaying())
				{
					this.sequence.Kill(false);
				}
				this.sequence = DOTween.Sequence().Append(this.rectTransform.DOScaleY(0f, 0.2f).SetEase(Ease.InCirc)).AppendCallback(delegate
				{
					this.repositioning = false;
					this.rectTransform.anchoredPosition = new Vector2(0f, newPos);
					this.parentCanvas.enabled = false;
				}).Insert(0f, this.parentCanvasGroup.DOFade(0f, 0.1f).SetEase(Ease.OutCirc)).Play<Sequence>();
				return true;
			}
			if (!this.parentCanvas.enabled || (flag && !this.repositioning))
			{
				if (!this.parentCanvas.enabled)
				{
					this.rectTransform.SetScaleY(0f);
				}
				this.buttonEnableDisable.gameObject.SetActive(false);
				this.currentTargetHeight = 0f;
			}
			this.artifactBonuses.text = artifact.effects[0].GetStringSelf(levelJump);
			this.contentSizeFitter.horizontalFit = LimittedContentSizeFitter.FitMode.Unconstrained;
			this.contentSizeFitter.verticalFit = LimittedContentSizeFitter.FitMode.PreferredSize;
			this.contentSizeFitter.SetLayoutVertical();
			this.textArtifactPercent.enabled = false;
			this.buttonReroll.textDown.text = string.Concat(new object[]
			{
				"UI_UPGRADE".Loc(),
				" (",
				levelJump,
				")"
			});
			this.textArtifactName.text = artifact.GetName();
			this.textArtifactLevel.text = artifact.GetMythicalLevelString();
			this.textArtifactLevel.color = PanelSelectedArtifact.MythicalArtifactLevelTextColor;
			this.textArtifactName.color = UiManager.colorArtifactLevels[artifact.GetLevel()];
			bool flag2 = artifact.IsLegendaryPlusMaxRanked();
			this.maxedIcon.gameObject.SetActive(flag2);
			this.buttonReroll.gameObject.SetActive(!flag2);
			this.buttonFiveX.gameObject.SetActive(!flag2);
			if (artifact.CanBeDisabled())
			{
				if (!this.buttonEnableDisable.gameObject.activeSelf)
				{
					this.buttonEnableDisable.transform.SetScale(0f);
					this.buttonEnableDisable.gameObject.SetActive(true);
					this.buttonEnableDisable.transform.DOScale(1f, 0.1f);
				}
				this.buttonEnableDisable.isOn = artifact.IsEnabled();
			}
			else if (this.buttonEnableDisable.gameObject.activeSelf)
			{
				this.buttonEnableDisable.transform.DOScale(0f, 0.1f).onComplete = delegate()
				{
					this.buttonEnableDisable.gameObject.SetActive(false);
				};
			}
			this.artifactBonuses.rectTransform.SetSizeDeltaX(670f);
			float newTargetHeight = this.artifactBonuses.preferredHeight + 250f;
			if (Mathf.Abs(this.currentTargetHeight - newTargetHeight) > 0.1f || flag)
			{
				this.hasSizeChanged = true;
				if (this.sequence != null && this.sequence.IsPlaying())
				{
					this.sequence.Kill(false);
				}
				this.sequence = DOTween.Sequence();
				this.sequence.AppendCallback(delegate
				{
					this.rectTransform.anchoredPosition = new Vector2(0f, newPos);
				});
				this.parentCanvasGroup.alpha = 1f;
				if (this.currentTargetHeight == 0f)
				{
					this.parentCanvas.enabled = true;
					this.sequence.AppendCallback(delegate
					{
						this.rectTransform.SetSizeDeltaY(newTargetHeight);
					});
					this.sequence.Append(this.rectTransform.DOScaleY(1f, 0.2f).SetEase(expandAnimationCurve));
				}
				else
				{
					this.rectTransform.SetScaleY(1f);
					this.sequence.Append(this.rectTransform.DOSizeDeltaY(newTargetHeight, 0.2f, false).SetEase(expandAnimationCurve));
				}
				this.sequence.Play<Sequence>();
				this.currentTargetHeight = newTargetHeight;
			}
			return false;
		}

		private void Awake()
		{
			this.parentCanvas.enabled = false;
		}

		private RectTransform m_rectTransform;

		public Text textArtifactName;

		public Text textArtifactLevel;

		public Text textArtifactPercent;

		public Text artifactBonuses;

		public ButtonUpgradeAnim buttonReroll;

		public RectTransform maxedIcon;

		public ButtonOnOff buttonEnableDisable;

		public LimittedContentSizeFitter contentSizeFitter;

		public GameButton buttonFiveX;

		public bool hasSizeChanged;

		public Canvas parentCanvas;

		public CanvasGroup parentCanvasGroup;

		[NonSerialized]
		public bool repositioning;

		[NonSerialized]
		public float currentTargetHeight;

		public Sequence sequence;

		private static readonly Color MythicalArtifactLevelTextColor = Color.white;
	}
}
