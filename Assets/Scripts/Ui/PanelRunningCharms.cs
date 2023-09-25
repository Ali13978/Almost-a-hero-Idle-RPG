using System;
using System.Collections.Generic;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelRunningCharms : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public void InitStrings()
		{
			this.noCharmWarning.text = LM.Get("UI_NO_CHARMS_SUMMONED");
		}

		public override void Init()
		{
			if (this.runningCharmCards != null)
			{
				foreach (CharmOptionCard obj in this.runningCharmCards)
				{
					UnityEngine.Object.Destroy(obj);
				}
			}
		}

		public void SetRiftEffects(ChallengeRift riftChallenge, UiManager uiManager)
		{
			int num = (riftChallenge == null || riftChallenge.riftEffects == null) ? 0 : riftChallenge.riftEffects.Count;
			Utility.FillUiElementList<Image>(this.imagePrefab, this.riftEffectsParent, num, this.riftEffectImages);
			for (int i = 0; i < num; i++)
			{
				this.riftEffectImages[i].sprite = uiManager.GetRiftEffectSprite(riftChallenge.riftEffects[i].GetType());
			}
		}

		public CharmOptionCard charmOptionCardPrefab;

		public Image imagePrefab;

		[NonSerialized]
		public List<Image> riftEffectImages = new List<Image>();

		[NonSerialized]
		public List<CharmOptionCard> runningCharmCards = new List<CharmOptionCard>();

		public RectTransform charmCardsParent;

		public RectTransform riftEffectsParent;

		public GameButton buttonRiftEffectsInfo;

		public ChildrenContentSizeFitter sizeFitter;

		public Text riftName;

		public RectTransform noCharmParent;

		public Text noCharmWarning;

		public Button openRiftEffectsInfoButton;
	}
}
