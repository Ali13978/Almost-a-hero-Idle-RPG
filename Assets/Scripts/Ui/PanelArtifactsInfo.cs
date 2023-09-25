using System;
using System.Collections.Generic;
using Simulation;
using Simulation.ArtifactSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelArtifactsInfo : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.InitStrings();
			this.closeButton.onClick = (this.closeButtonBG.onClick = delegate()
			{
				this.OnClose();
			});
			this.effectWidgets = new List<ArtifactEffectInfoWidget>();
		}

		public void InitStrings()
		{
			this.textHeader.text = LM.Get("UI_ARTIFACTS_INFO_HEADER");
			this.totalArtifactLevelLabel.text = "UI_ARTIFACTS_TOTAL_ARTIFACTS_LEVEL".Loc();
			this.maxArtifactLevelLabel.text = "UI_ARTIFACTS_QP_PER_ARTIFACT".Loc();
			this.commonEffectsHeader.text = "UI_COMMON_EFFECTS".Loc();
			this.uniqueEffectsHeader.text = "UI_UNIQUE_STATS".Loc();
			this.mythicalEffectsHeader.text = "UI_MYTHICAL_EFFECTS".Loc();
		}

		public void SetLayoutDirty()
		{
			this.talContent.SetLayoutHorizontal();
			this.malContent.SetLayoutHorizontal();
			LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)this.talContent.transform);
			LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)this.malContent.transform);
		}

		public void SetPlacement(Simulator simulator)
		{
			ArtifactsManager artifactsManager = simulator.artifactsManager;
			this.totalArtifactLevel.text = artifactsManager.TotalArtifactsLevel.ToString();
			this.maxArtifactLevel.text = artifactsManager.GetMaxLevelCap().ToString();
			int num = EffectIds.Common.Length;
			List<KeyValuePair<int, int>> infoOfAllUniqueEffectsAvailableAtCurrentTotalLevelMilestone = artifactsManager.GetInfoOfAllUniqueEffectsAvailableAtCurrentTotalLevelMilestone();
			int count = infoOfAllUniqueEffectsAvailableAtCurrentTotalLevelMilestone.Count;
			List<KeyValuePair<int, int>> effectUnlocksInfoForNextTotalLevelMilestone = artifactsManager.GetEffectUnlocksInfoForNextTotalLevelMilestone();
			int num2 = (effectUnlocksInfoForNextTotalLevelMilestone != null) ? effectUnlocksInfoForNextTotalLevelMilestone.Count : 0;
			List<Simulation.Artifact> mythicalArtifacts = simulator.artifactsManager.MythicalArtifacts;
			int count2 = mythicalArtifacts.Count;
			int num3 = num + count + count2 + num2;
			if (num3 != this.effectWidgets.Count)
			{
				Utility.FillUiElementList<ArtifactEffectInfoWidget>(this.artifactEffectInfoPrefab, this.effectsParent, num3, this.effectWidgets);
			}
			this.uniqueEffectsInventory.text = artifactsManager.ownedUniqueEffectStock + "/" + artifactsManager.totalUniqueEffectStock;
			float num4 = this.contentPadding;
			this.commonEffectsHeaderParent.SetAnchorPosY(-num4);
			num4 += this.commonEffectsHeaderParent.sizeDelta.y;
			for (int i = 0; i < EffectIds.Common.Length; i++)
			{
				ArtifactEffectInfoWidget artifactEffectInfoWidget = this.effectWidgets[--num3];
				int effectId = EffectIds.Common[i];
				artifactEffectInfoWidget.SetTextAlpha(1f);
				artifactEffectInfoWidget.rectTransform.anchoredPosition = new Vector2(0f, -num4 - this.gapBetweenRows);
				artifactEffectInfoWidget.SetCommonEffect(effectId, artifactsManager.GetCommonEffectTotalValue(effectId, simulator.GetUniversalBonusAll()), new Color?((i % 2 != 0) ? this.oddRowsBackgroundColor : this.evenRowsBackgroundColor), this.effectMaxedStateWidgetInfo);
				num4 += artifactEffectInfoWidget.rectTransform.sizeDelta.y + this.gapBetweenRows;
			}
			num4 += this.gapBetweenCategories;
			this.uniqueEffectsHeaderParent.SetAnchorPosY(-num4);
			num4 += this.uniqueEffectsHeaderParent.sizeDelta.y;
			for (int j = 0; j < count; j++)
			{
				ArtifactEffectInfoWidget artifactEffectInfoWidget2 = this.effectWidgets[--num3];
				KeyValuePair<int, int> keyValuePair = infoOfAllUniqueEffectsAvailableAtCurrentTotalLevelMilestone[j];
				artifactEffectInfoWidget2.SetTextAlpha(1f);
				artifactEffectInfoWidget2.rectTransform.anchoredPosition = new Vector2(0f, -num4 - this.gapBetweenRows);
				artifactEffectInfoWidget2.SetUniqueEffect(simulator.GetUniversalBonusAll(), keyValuePair.Key, artifactsManager.GetCurrentCopiesOfUniqueEffect(keyValuePair.Key), keyValuePair.Value, new Color?((j % 2 != 0) ? this.oddRowsBackgroundColor : this.evenRowsBackgroundColor), this.effectMaxedStateWidgetInfo);
				num4 += artifactEffectInfoWidget2.rectTransform.sizeDelta.y + this.gapBetweenRows;
			}
			if (num2 > 0)
			{
				this.nextEffectsHeaderParent.gameObject.SetActive(true);
				num4 += this.gapBetweenCategories;
				this.nextEffectsHeaderParent.SetAnchorPosY(-num4);
				num4 += this.nextEffectsHeaderParent.sizeDelta.y;
				num4 += this.gapBetweenRows;
				string str = " (" + string.Format("UI_TOTAL_ARTIFACT_LEVEL_FORMAT".Loc(), artifactsManager.GetTotalArtifactsLevelOfNextMilestone()) + ")";
				this.nextEffectsHeader.text = "UI_NEXT_EFFECTS".Loc() + str;
				for (int k = 0; k < num2; k++)
				{
					ArtifactEffectInfoWidget artifactEffectInfoWidget3 = this.effectWidgets[--num3];
					KeyValuePair<int, int> keyValuePair2 = effectUnlocksInfoForNextTotalLevelMilestone[k];
					artifactEffectInfoWidget3.rectTransform.anchoredPosition = new Vector2(0f, -num4 - this.gapBetweenRows);
					artifactEffectInfoWidget3.SetUniqueEffect(simulator.GetUniversalBonusAll(), keyValuePair2.Key, keyValuePair2.Value, true, new Color?((k % 2 != 0) ? this.oddRowsBackgroundColor : this.evenRowsBackgroundColor), this.effectMaxedStateWidgetInfo);
					num4 += artifactEffectInfoWidget3.rectTransform.sizeDelta.y + this.gapBetweenRows;
					artifactEffectInfoWidget3.SetTextAlpha(0.4f);
				}
			}
			else
			{
				this.nextEffectsHeaderParent.gameObject.SetActive(false);
			}
			if (count2 > 0)
			{
				this.mythicalEffectsHeaderParent.gameObject.SetActive(true);
				num4 += this.gapBetweenCategories;
				this.mythicalEffectsHeaderParent.SetAnchorPosY(-num4);
				num4 += this.mythicalEffectsHeaderParent.sizeDelta.y;
				for (int l = 0; l < count2; l++)
				{
					ArtifactEffectInfoWidget artifactEffectInfoWidget4 = this.effectWidgets[--num3];
					artifactEffectInfoWidget4.SetTextAlpha(1f);
					artifactEffectInfoWidget4.rectTransform.anchoredPosition = new Vector2(0f, -num4 - this.gapBetweenRows);
					artifactEffectInfoWidget4.SetMythicalEffect(mythicalArtifacts[l], new Color?((l % 2 != 0) ? this.oddRowsBackgroundColor : this.evenRowsBackgroundColor), this.effectMaxedStateWidgetInfo);
					num4 += artifactEffectInfoWidget4.rectTransform.sizeDelta.y + this.gapBetweenRows;
				}
			}
			else
			{
				this.mythicalEffectsHeaderParent.gameObject.SetActive(false);
			}
			num4 += this.contentPadding;
			this.effectsParent.SetSizeDeltaY(num4);
			this.contentParent.SetSizeDeltaY(Mathf.Abs(this.effectsParent.anchoredPosition.y) + this.effectsParent.sizeDelta.y);
		}

		public Action OnClose;

		[NonSerialized]
		public UiState stateToReturn;

		[SerializeField]
		private GameButton closeButton;

		[SerializeField]
		private GameButton closeButtonBG;

		[SerializeField]
		private Text textHeader;

		[SerializeField]
		private Text totalArtifactLevelLabel;

		[SerializeField]
		private Text totalArtifactLevel;

		[SerializeField]
		private Text maxArtifactLevelLabel;

		[SerializeField]
		private Text maxArtifactLevel;

		[SerializeField]
		private RectTransform commonEffectsHeaderParent;

		[SerializeField]
		private Text commonEffectsHeader;

		[SerializeField]
		private RectTransform mythicalEffectsHeaderParent;

		[SerializeField]
		private Text mythicalEffectsHeader;

		[SerializeField]
		private RectTransform uniqueEffectsHeaderParent;

		[SerializeField]
		private Text uniqueEffectsHeader;

		[SerializeField]
		private Text uniqueEffectsInventory;

		[SerializeField]
		private RectTransform nextEffectsHeaderParent;

		[SerializeField]
		private Text nextEffectsHeader;

		[SerializeField]
		private ArtifactEffectInfoWidget artifactEffectInfoPrefab;

		[SerializeField]
		private RectTransform contentParent;

		[SerializeField]
		private RectTransform effectsParent;

		[SerializeField]
		private float contentPadding = 10f;

		[SerializeField]
		private float gapBetweenRows = 5f;

		[SerializeField]
		private float gapBetweenCategories = 15f;

		[SerializeField]
		private Color evenRowsBackgroundColor;

		[SerializeField]
		private Color oddRowsBackgroundColor;

		[SerializeField]
		private ArtifactEffectInfoWidget.MaxedStateInfo effectMaxedStateWidgetInfo;

		[SerializeField]
		private HorizontalLayoutGroup talContent;

		[SerializeField]
		private HorizontalLayoutGroup malContent;

		private List<ArtifactEffectInfoWidget> effectWidgets;
	}
}
