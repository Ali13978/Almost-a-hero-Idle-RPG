using System;
using System.Collections.Generic;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelRiftEffectInfo : MonoBehaviour
	{
		public void InitStrings()
		{
			this.buttonOkay.text.text = LM.Get("UI_OKAY");
			this.textDescription.text = LM.Get("RIFT_EFFECT_INFO_DESC");
			this.textHeader.text = LM.Get("RIFT_EFFECT_INFO_HEADER");
		}

		public void SetRiftEffects(ChallengeRift cr, UiManager manager)
		{
			this.textHeader.text = LM.Get("RIFT_EFFECT_INFO_HEADER");
			this.textDescription.text = LM.Get("RIFT_EFFECT_INFO_DESC");
			int count = cr.riftEffects.Count;
			Utility.FillUiElementList<RiftEffectDescription>(this.riftEffectDescriptionPrefab, this.effectsParent, count, this.riftEffectDescriptions);
			for (int i = 0; i < count; i++)
			{
				RiftEffectDescription riftEffectDescription = this.riftEffectDescriptions[i];
				RiftEffect riftEffect = cr.riftEffects[i];
				riftEffectDescription.riftEffectImage.icon.sprite = manager.GetRiftEffectSprite(riftEffect.GetType());
				riftEffectDescription.description.text = riftEffect.GetDesc();
				riftEffectDescription.curseParent.SetActive(false);
				riftEffectDescription.description.enabled = true;
				riftEffectDescription.riftEffectImage.background.color = new Color(0f, 0f, 0f, 0.0392156877f);
				riftEffectDescription.riftEffectImage.icon.color = Color.white;
				riftEffectDescription.rectTransform.anchoredPosition = new Vector2(0f, -12.5f - 135f * (float)i);
			}
			float y = 592.5f + 135f * (float)(count - 1);
			this.popupParent.SetSizeDeltaY(y);
		}

		public void SetCurseffects(UiManager manager)
		{
			this.textHeader.text = LM.Get("CURRENT_CURSES");
			this.textDescription.text = LM.Get("RIFT_EFFECT_CURSED_GATE");
			int count = manager.sim.currentCurses.Count;
			Utility.FillUiElementList<RiftEffectDescription>(this.riftEffectDescriptionPrefab, this.effectsParent, count, this.riftEffectDescriptions);
			for (int i = 0; i < count; i++)
			{
				RiftEffectDescription riftEffectDescription = this.riftEffectDescriptions[i];
				int num = manager.sim.currentCurses[i];
				CurseEffectData curseEffectData = manager.sim.allCurseEffects[num];
				riftEffectDescription.riftEffectImage.icon.sprite = manager.spritesCurseEffectIcon[num];
				riftEffectDescription.riftEffectImage.icon.color = ((num != 1019) ? PanelRiftEffectInfo.CurseIconColor : PanelRiftEffectInfo.GhostHeroesIconColor);
				riftEffectDescription.riftEffectImage.background.color = ((num != 1019) ? PanelRiftEffectInfo.CurseBackgroundColor : PanelRiftEffectInfo.GhostHeroesBackgroundColor);
				int level = curseEffectData.level;
				curseEffectData.level = 0;
				riftEffectDescription.curseDescription.text = curseEffectData.GetDesc();
				riftEffectDescription.curseDispel.text = string.Format("{0} {1}", curseEffectData.GetConditionDescFormat(), curseEffectData.GetConditionDescriptionNoColor());
				riftEffectDescription.description.enabled = false;
				riftEffectDescription.curseParent.SetActive(true);
				curseEffectData.level = level;
				riftEffectDescription.rectTransform.anchoredPosition = new Vector2(0f, -12.5f - 150f * (float)i);
			}
			float y = 592.5f + 150f * (float)(count - 1);
			this.popupParent.SetSizeDeltaY(y);
		}

		public Text textHeader;

		public Text textDescription;

		public GameButton buttonOkay;

		public GameButton buttonBackground;

		public RectTransform popupParent;

		public RectTransform effectsParent;

		public RiftEffectDescription riftEffectDescriptionPrefab;

		public List<RiftEffectDescription> riftEffectDescriptions;

		public UiState stateToReturn;

		private static readonly Color CurseIconColor = new Color(0.7058824f, 0.1843137f, 0.1215686f, 1f);

		private static readonly Color GhostHeroesIconColor = new Color(0.549019635f, 0.309803933f, 0.75686276f);

		private static readonly Color CurseBackgroundColor = new Color(0.2666667f, 0.08627451f, 0.08627451f, 1f);

		private static readonly Color GhostHeroesBackgroundColor = new Color(0.192156866f, 0.09803922f, 0.266666681f, 1f);
	}
}
