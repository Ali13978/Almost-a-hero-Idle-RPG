using System;
using Simulation;
using Simulation.ArtifactSystem;
using Static;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class ArtifactEffectInfoWidget : MonoBehaviour
	{
		public void SetTextAlpha(float alpha)
		{
			this.levelOrCopies.SetAlpha(alpha);
			this.description.SetAlpha(alpha);
			this.effectValue.SetAlpha(alpha);
		}

		public void SetUniqueEffect(UniversalTotalBonus universalTotalBonus, int effectId, int copiesAmount, bool effectObtained, Color? backgroundColor = null, ArtifactEffectInfoWidget.MaxedStateInfo maxedStateInfo = null)
		{
			this.InitIfNecessary();
			this.levelOrCopies.enabled = true;
			this.levelOrCopies.text = StringExtension.Concat((!effectObtained) ? "x" : "+", copiesAmount.ToString());
			this.description.rectTransform.SetAnchorPosX(this.descriptionMidlePos);
			this.SetEffect(EffectsDatabase.Unique[effectId].Effect, universalTotalBonus, backgroundColor, false, 1);
			this.SetMaxedStateInfo(maxedStateInfo, false);
		}

		public void SetCommonEffect(int effectId, double effectValue, Color? backgroundColor = null, ArtifactEffectInfoWidget.MaxedStateInfo maxedStateInfo = null)
		{
			this.InitIfNecessary();
			this.levelOrCopies.enabled = false;
			this.description.rectTransform.SetAnchorPosX(15f);
			this.description.text = EffectsDatabase.Common[effectId].GetDescription();
			this.effectValue.text = "+" + GameMath.GetPercentString(effectValue, false);
			this.SetMaxedStateInfo(maxedStateInfo, false);
		}

		public void SetUniqueEffect(UniversalTotalBonus universalTotalBonus, int effectId, int copiesAmount, int maxCopies, Color? backgroundColor = null, ArtifactEffectInfoWidget.MaxedStateInfo maxedStateInfo = null)
		{
			this.InitIfNecessary();
			this.levelOrCopies.enabled = true;
			this.levelOrCopies.text = StringExtension.Concat(copiesAmount.ToString(), "/", maxCopies.ToString());
			this.description.rectTransform.SetAnchorPosX(this.descriptionMidlePos);
			this.SetEffect(EffectsDatabase.Unique[effectId].Effect, universalTotalBonus, backgroundColor, true, copiesAmount);
			this.SetMaxedStateInfo(maxedStateInfo, copiesAmount == maxCopies);
		}

		public void SetMythicalEffect(Simulation.Artifact artifact, Color? backgroundColor = null, ArtifactEffectInfoWidget.MaxedStateInfo maxedStateInfo = null)
		{
			this.InitIfNecessary();
			this.levelOrCopies.enabled = false;
			this.effectValue.text = artifact.GetMythicalLevelStringSimple();
			this.description.rectTransform.SetAnchorPosX(this.descriptionLeftPos);
			this.description.text = artifact.GetName();
			this.SetMaxedStateInfo(maxedStateInfo, artifact.IsLegendaryPlusMaxRanked());
			if (backgroundColor != null)
			{
				this.background.color = backgroundColor.Value;
			}
		}

		private void SetEffect(Simulation.ArtifactSystem.ArtifactEffect effect, UniversalTotalBonus universalTotalBonus, Color? backgroundColor, bool forcePercentage = false, int copiesAmount = 1)
		{
			this.description.text = effect.GetDescription();
			this.effectValue.text = ((!forcePercentage) ? effect.GetSignedValue(copiesAmount, universalTotalBonus) : effect.GetSignedValueUsingPercentage(copiesAmount, universalTotalBonus));
			if (backgroundColor != null)
			{
				this.background.color = backgroundColor.Value;
			}
		}

		private void SetMaxedStateInfo(ArtifactEffectInfoWidget.MaxedStateInfo maxedStateInfo, bool isMaxed)
		{
			if (maxedStateInfo != null)
			{
				Color color = (!isMaxed) ? maxedStateInfo.notMaxedTextColor : maxedStateInfo.maxedTextColor;
				this.levelOrCopies.color = color;
				this.description.color = color;
				this.effectValue.color = color;
			}
		}

		private void InitIfNecessary()
		{
			if (!this.initialized)
			{
				this.initialized = true;
				this.descriptionLeftPos = this.levelOrCopies.rectTransform.anchoredPosition.x;
				this.descriptionMidlePos = this.description.rectTransform.anchoredPosition.x;
			}
		}

		public RectTransform rectTransform;

		public CanvasGroup canvasGroup;

		[SerializeField]
		private Text levelOrCopies;

		[SerializeField]
		private Text description;

		[SerializeField]
		private Text effectValue;

		[SerializeField]
		private Image background;

		private float descriptionLeftPos;

		private float descriptionMidlePos;

		private bool initialized;

		[Serializable]
		public class MaxedStateInfo
		{
			public Color maxedTextColor;

			public Color notMaxedTextColor;
		}
	}
}
