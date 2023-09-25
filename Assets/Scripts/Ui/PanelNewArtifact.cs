using System;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelNewArtifact : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.csfArtifactBonuses = this.textArtifactBonuses.GetComponent<ContentSizeFitter>();
		}

		public void InitStrings()
		{
			this.buttonSelect.text.text = LM.Get("UI_SELECT");
			this.textQP.text = "UI_HEROES_LV".Loc();
		}

		public void SetDetails(Artifact artifact, Sprite iconSprite)
		{
			int level = artifact.GetLevel();
			if (artifact.IsLegendaryPlus())
			{
				this.textQuality.text = (artifact.GetLegendaryPlusRank() + 1).ToString();
				this.csfArtifactBonuses.enabled = false;
				this.textArtifactBonuses.resizeTextForBestFit = true;
				this.textArtifactPercent.enabled = false;
				this.textArtifactName.color = UiManager.colorArtifactLevels[level];
				this.textArtifactName.text = artifact.GetName();
				this.textArtifactLevel.text = string.Empty;
				this.textArtifactPercent.text = string.Empty;
				this.textArtifactBonuses.text = artifact.effects[0].GetStringSelf(1);
				this.textArtifactBonuses.rectTransform.sizeDelta = this.textArtifactBonusSize;
			}
			else
			{
				this.textQuality.text = GameMath.GetDoubleString(artifact.GetQuality());
				this.csfArtifactBonuses.enabled = true;
				this.textArtifactBonuses.resizeTextForBestFit = false;
				this.textArtifactPercent.enabled = true;
				this.textArtifactName.color = this.colorTextArtifactName;
				this.textArtifactName.text = artifact.GetName();
				this.textArtifactLevel.text = UiManager.ArtifactLevelString(level);
				this.textArtifactLevel.color = UiManager.colorArtifactLevels[level];
				this.textArtifactPercent.text = string.Empty;
				this.textArtifactBonuses.text = string.Empty;
				foreach (ArtifactEffect artifactEffect in artifact.effects)
				{
					Text text = this.textArtifactPercent;
					text.text = text.text + artifactEffect.GetAmountString() + "\n";
					string stringSelf = artifactEffect.GetStringSelf(1);
					Text text2 = this.textArtifactBonuses;
					text2.text = text2.text + stringSelf + "\n";
				}
				this.textArtifactPercent.text = this.textArtifactPercent.text.Substring(0, this.textArtifactPercent.text.Length - 1);
				this.textArtifactBonuses.text = this.textArtifactBonuses.text.Substring(0, this.textArtifactBonuses.text.Length - 1);
			}
			this.artifactStone.imageIcon.sprite = iconSprite;
			this.artifactStone.imageIcon.SetNativeSize();
			this.artifactStone.PlaySpineAnim(level - 1);
		}

		public void SetAlpha(float alpha)
		{
			this.canvasGroup.alpha = alpha;
		}

		public void SetStoneBoxAlpha(float alpha)
		{
			this.stoneBoxCanvasGroup.alpha = alpha;
		}

		public CanvasGroup canvasGroup;

		public Image imageBg;

		public ArtifactStone artifactStone;

		public Text textQuality;

		public Image imageStoneHolder;

		public Text textTitle;

		public Text textQP;

		public Text textArtifactName;

		public Text textArtifactLevel;

		public Text textArtifactPercent;

		public Text textArtifactBonuses;

		private ContentSizeFitter csfArtifactBonuses;

		public GameButton buttonSelect;

		public CanvasGroup stoneBoxCanvasGroup;

		public Vector2 posButtonEdge;

		public Color colorTextArtifactName;

		public Vector2 textArtifactBonusSize;
	}
}
