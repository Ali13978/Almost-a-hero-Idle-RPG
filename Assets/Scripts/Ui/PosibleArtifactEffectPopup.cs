using System;
using System.Collections.Generic;
using Simulation.ArtifactSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PosibleArtifactEffectPopup : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.buttonClose.onClick = (this.buttonCloseBg.onClick = new GameButton.VoidFunc(this.Button_CloseClicked));
		}

		private void Button_CloseClicked()
		{
			this.manager.state = this.stateToReturn;
			UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiPopupDisappear, 1f));
		}

		public void InitStrings()
		{
			this.textHeader.text = "POSSIBLE_EFFECTS_HEADER".Loc();
			this.textDescription.text = "POSSIBLE_EFFECTS_DESC".Loc();
		}

		public void SetPossibleEffects()
		{
			ArtifactsManager artifactsManager = this.manager.sim.artifactsManager;
			List<int> copyCountsOfAllUniqueEffects = artifactsManager.GetCopyCountsOfAllUniqueEffects();
			Utility.FillUiElementList<ArtifactEffectLabel>(this.artifactEffectLabelPrefab, this.effectsParent, copyCountsOfAllUniqueEffects.Count, this.artifactEffectLabels);
			int num = 40;
			float num2 = 140f;
			for (int i = 0; i < copyCountsOfAllUniqueEffects.Count; i++)
			{
				ArtifactEffectLabel artifactEffectLabel = this.artifactEffectLabels[i];
				int effectId = copyCountsOfAllUniqueEffects[i];
				num2 += (float)num;
				artifactEffectLabel.rectTransform.anchoredPosition = new Vector2(0f, (float)(-(float)num * i));
				if (i % 2 == 0)
				{
					artifactEffectLabel.EnableBackground();
				}
				else
				{
					artifactEffectLabel.DisableBackground();
				}
				artifactEffectLabel.attributeDesc.text = artifactsManager.GetDescriptionOfEffectWithId(effectId);
				artifactEffectLabel.attributePercent.text = artifactsManager.GetUniqueEffectBaseSignedValue(effectId, this.manager.sim.GetUniversalBonusAll());
			}
			num2 += 10f;
			this.scrollRect.content.SetSizeDeltaY(num2);
		}

		private const float DescriptionHeight = 140f;

		public GameButton buttonClose;

		public GameButton buttonCloseBg;

		public ArtifactEffectLabel artifactEffectLabelPrefab;

		public List<ArtifactEffectLabel> artifactEffectLabels;

		public ScrollRect scrollRect;

		public RectTransform effectsParent;

		public Text textHeader;

		public Text textDescription;

		public UiManager manager;

		public UiState stateToReturn;
	}
}
