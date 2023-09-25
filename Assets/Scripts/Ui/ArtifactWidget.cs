using System;
using Simulation;
using Simulation.ArtifactSystem;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class ArtifactWidget : MonoBehaviour
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

		public void SetDetails(Simulation.ArtifactSystem.Artifact artifact, UniversalTotalBonus universalTotalBonus)
		{
			this.rectTransform.SetSizeDeltaY(this.regularHeight);
			this.regularArtifactDescParent.gameObject.SetActive(true);
			this.mythicalArtifactDesc.gameObject.SetActive(false);
			this.artifactQPLoc.text = "UI_HEROES_LV".Loc();
			this.artifactQP.text = artifact.Level.ToString();
			this.artifactNameLabel.enabled = false;
			this.uniqueStatDesc.text = EffectsDatabase.Common[artifact.CommonEffectId].GetDescriptionWithValue(artifact.Level, universalTotalBonus);
		}

		public void SetDetails(Simulation.Artifact artifact)
		{
			this.rectTransform.SetSizeDeltaY(this.mythicalHeight);
			this.artifactNameLabel.enabled = true;
			this.regularArtifactDescParent.gameObject.SetActive(false);
			this.artifactQPLoc.text = "UI_HEROES_LV".Loc();
			this.artifactNameLabel.text = artifact.GetName();
			this.artifactQP.text = (artifact.GetLegendaryPlusRank() + 1).ToString();
			this.mythicalArtifactDesc.gameObject.SetActive(true);
			this.mythicalArtifactDesc.text = artifact.effects[0].GetStringSelf(1);
		}

		private RectTransform m_rectTransform;

		public Text artifactNameLabel;

		public CanvasGroup canvasGroup;

		public CanvasGroup artifactSlot;

		public Text artifactQP;

		public Text artifactQPLoc;

		public Text mythicalArtifactDesc;

		public ArtifactStone artifactStone;

		public GameButton button;

		public RectTransform attributeParent;

		public Image selectionOutline;

		public RectTransform artifactHolderRect;

		public RectTransform selectionParticleParent;

		public SkeletonGraphic[] particlePlayers;

		public RectTransform regularArtifactDescParent;

		public Text uniqueStatDesc;

		public float mythicalHeight;

		public float regularHeight;
	}
}
