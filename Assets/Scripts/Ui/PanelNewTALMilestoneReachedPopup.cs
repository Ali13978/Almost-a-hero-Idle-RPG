using System;
using System.Collections.Generic;
using DG.Tweening;
using Simulation;
using Simulation.ArtifactSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelNewTALMilestoneReachedPopup : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.effectObtainedRows = new List<ArtifactEffectInfoWidget>();
			this.nextEffectRows = new List<ArtifactEffectInfoWidget>();
			this.closeButton.onClick = delegate()
			{
				this.OnClose();
			};
		}

		public void InitStrings()
		{
			this.closeButton.text.text = LM.Get("UI_OKAY");
			this.header.text = LM.Get("UI_TAL_MILESTONE_REACHED_POPUP_HEADER");
			this.description.text = LM.Get("UI_TAL_MILESTONE_DESCRIPTION");
		}

		public void OnPopupAppeared(UiState previousState, Simulator simulator)
		{
			this.previousState = previousState;
			ArtifactsManager artifactsManager = simulator.artifactsManager;
			artifactsManager.UpdateCurrentTotalArtifactsLevelMilestone();
			float num = Mathf.Abs(this.effectsObtainedParent.anchoredPosition.y);
			List<KeyValuePair<int, int>> effectUnlocksInfoForCurrentTotalLevelMilestone = artifactsManager.GetEffectUnlocksInfoForCurrentTotalLevelMilestone();
			int num2 = this.SetRows(simulator, effectUnlocksInfoForCurrentTotalLevelMilestone, this.effectObtainedRows, this.effectsObtainedFirstRowPos, true, this.effectObtainedRowPrefab, this.effectsObtainedParent);
			this.effectsObtainedParent.SetSizeDeltaY(Mathf.Abs(this.effectsObtainedFirstRowPos) + (float)num2 * (this.gapBetweenRows + this.effectObtainedRowPrefab.rectTransform.sizeDelta.y) - this.gapBetweenRows + this.containersBottomPadding);
			this.currentTALMilestone.text = string.Format(LM.Get("UI_TOTAL_ARTIFACT_LEVEL_FORMAT", artifactsManager.GetTotalArtifactsLevelOfCurrentMilestone()), new object[0]);
			num += this.effectsObtainedParent.sizeDelta.y;
			num += this.buttonSpaceNeeded;
			this.popupParent.SetSizeDeltaY(num);
			this.DoAppearAnimation();
		}

		private int SetRows(Simulator simulator, List<KeyValuePair<int, int>> effectUnlocksInfo, List<ArtifactEffectInfoWidget> rows, float posOffset, bool effectsObtained, ArtifactEffectInfoWidget rowPrefab, RectTransform rowsParent)
		{
			int num = 0;
			int count = effectUnlocksInfo.Count;
			for (int i = 0; i < count; i++)
			{
				num += effectUnlocksInfo[i].Value;
			}
			if (count != rows.Count)
			{
				Utility.FillUiElementList<ArtifactEffectInfoWidget>(rowPrefab, rowsParent, num, rows);
			}
			int num2 = 0;
			for (int j = 0; j < count; j++)
			{
				KeyValuePair<int, int> keyValuePair = effectUnlocksInfo[j];
				for (int k = 0; k < keyValuePair.Value; k++)
				{
					ArtifactEffectInfoWidget artifactEffectInfoWidget = rows[num2];
					artifactEffectInfoWidget.SetUniqueEffect(simulator.GetUniversalBonusAll(), keyValuePair.Key, keyValuePair.Value, effectsObtained, null, null);
					artifactEffectInfoWidget.rectTransform.anchoredPosition = new Vector2(0f, posOffset - (artifactEffectInfoWidget.rectTransform.sizeDelta.y + this.gapBetweenRows) * (float)num2);
					num2++;
				}
			}
			return num;
		}

		[InspectButton]
		public void DoAppearAnimation()
		{
			float y = this.effectsObtainedParent.anchoredPosition.y;
			this.effectsObtainedParent.SetAnchorPosY(600f);
			this.closeButtonParent.SetScale(0f);
			Sequence sequence = DOTween.Sequence().Append(this.effectsObtainedParent.DOAnchorPosY(y, 0.2f, false));
			for (int i = 0; i < this.effectObtainedRows.Count; i++)
			{
				ArtifactEffectInfoWidget artifactEffectInfoWidget = this.effectObtainedRows[i];
				artifactEffectInfoWidget.canvasGroup.alpha = 0f;
				artifactEffectInfoWidget.rectTransform.SetAnchorPosX(-10f);
				sequence.Insert(0.4f + (float)i * 0.05f, artifactEffectInfoWidget.rectTransform.DOAnchorPosX(0f, 0.2f, false).SetEase(Ease.OutQuart)).Insert(0.4f + (float)i * 0.05f, artifactEffectInfoWidget.canvasGroup.DOFade(1f, 0.2f).SetEase(Ease.OutQuart));
			}
			for (int j = 0; j < this.nextEffectRows.Count; j++)
			{
				ArtifactEffectInfoWidget artifactEffectInfoWidget2 = this.nextEffectRows[j];
				artifactEffectInfoWidget2.canvasGroup.alpha = 0f;
				artifactEffectInfoWidget2.rectTransform.SetAnchorPosX(-10f);
				sequence.Insert(0.6f + (float)j * 0.05f, artifactEffectInfoWidget2.rectTransform.DOAnchorPosX(0f, 0.2f, false).SetEase(Ease.OutQuart)).Insert(0.6f + (float)j * 0.05f, artifactEffectInfoWidget2.canvasGroup.DOFade(1f, 0.2f).SetEase(Ease.OutQuart));
			}
			sequence.Append(this.closeButtonParent.DOScale(1f, 0.4f).SetEase(Ease.OutBack));
			sequence.Play<Sequence>();
		}

		public Action OnClose;

		[NonSerialized]
		public UiState previousState;

		[SerializeField]
		private Text header;

		[SerializeField]
		private GameButton closeButton;

		[SerializeField]
		private RectTransform closeButtonParent;

		[SerializeField]
		private RectTransform popupParent;

		[SerializeField]
		private float gapBetweenRows;

		[SerializeField]
		private float containersBottomPadding;

		[SerializeField]
		private float buttonSpaceNeeded;

		[SerializeField]
		private float gapBetweenContainers;

		[Header("Effects Obtained Container")]
		[SerializeField]
		private RectTransform effectsObtainedParent;

		[SerializeField]
		private CanvasGroup effectsObtainedCanvas;

		[SerializeField]
		private Text currentTALMilestone;

		[SerializeField]
		private Text description;

		[SerializeField]
		private float effectsObtainedFirstRowPos;

		[SerializeField]
		private ArtifactEffectInfoWidget effectObtainedRowPrefab;

		private List<ArtifactEffectInfoWidget> effectObtainedRows;

		private List<ArtifactEffectInfoWidget> nextEffectRows;
	}
}
