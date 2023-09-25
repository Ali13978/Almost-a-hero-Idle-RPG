using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelHeroes : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.heroPanelFirstPos = this.heroPanels[0].GetComponent<RectTransform>().anchoredPosition;
			this.heroPanelDeltaPos = this.heroPanels[1].GetComponent<RectTransform>().anchoredPosition - this.heroPanels[0].GetComponent<RectTransform>().anchoredPosition;
			this.InitStrings();
		}

		public void InitStrings()
		{
			this.buttonOpenNewHeroPanel.textDown.text = LM.Get("UI_HEROES_GET_NEW");
			this.panelWorldUpgrade.buttonUpgradeAnim.textDown.text = LM.Get("UI_UPGRADE");
			this.panelWorldUpgrade.textCantUpgrade.text = LM.Get("UI_UPGRADE");
			this.panelTotem.textDmg.text = LM.Get("UI_HEROES_DMG");
			this.textAutoSkillDesc.text = LM.Get("UI_AUTO_SKILL_DISTRIBUTE");
			int i = 0;
			int count = this.heroPanels.Count;
			while (i < count)
			{
				this.heroPanels[i].textDmg.text = LM.Get("UI_HEROES_DMG");
				this.heroPanels[i].textHp.text = LM.Get("UI_HEROES_HP");
				i++;
			}
		}

		public void SetNumHeroes(int numHeroes, int maxNumHeroes, bool adventureRandomHeroesEnabled)
		{
			for (int i = this.heroPanels.Count - 1; i >= 0; i--)
			{
				this.heroPanels[i].gameObject.SetActive(i < numHeroes);
			}
			float num = (float)((!this.isTotemSelected) ? -1 : 0);
			int j = 0;
			int count = this.heroPanels.Count;
			while (j < count)
			{
				this.heroPanels[j].rectTransform.anchoredPosition = this.heroPanelFirstPos + ((float)j + num) * this.heroPanelDeltaPos;
				j++;
			}
			if (numHeroes >= maxNumHeroes || !TutorialManager.ShouldShowNewHeroButton())
			{
				this.buttonOpenNewHeroPanel.gameObject.SetActive(false);
				this.buttonBuyRandomHero.gameObject.SetActive(false);
				this.autoSkillParent.SetAnchorPosY((!adventureRandomHeroesEnabled) ? -9f : this.GetPositionAfterLastHero(numHeroes));
				return;
			}
			this.buttonOpenNewHeroPanel.gameObject.SetActive(true);
			this.buttonOpenNewHeroPanel.rectTransform.SetSizeDeltaX((float)((!adventureRandomHeroesEnabled) ? 564 : 426));
			this.buttonBuyRandomHero.gameObject.SetActive(adventureRandomHeroesEnabled);
			float positionAfterLastHero = this.GetPositionAfterLastHero(numHeroes);
			this.buttonOpenNewHeroPanel.rectTransform.SetAnchorPosY(positionAfterLastHero - 50f);
			this.buttonBuyRandomHero.rectTransform.SetAnchorPosY(positionAfterLastHero - 50f);
			this.autoSkillParent.SetAnchorPosY((!adventureRandomHeroesEnabled) ? -9f : (positionAfterLastHero - 130f));
		}

		private float GetPositionAfterLastHero(int numHeroes)
		{
			float num;
			if (numHeroes == 0)
			{
				num = this.heroPanels[0].rectTransform.anchoredPosition.y;
				num -= 25f;
			}
			else
			{
				num = this.heroPanels[numHeroes - 1].rectTransform.anchoredPosition.y;
				num -= 180f;
			}
			return num;
		}

		public bool isTotemSelected;

		public bool isMilestoneEnabled;

		public PanelHero panelTotem;

		public PanelWorldUpgrade panelWorldUpgrade;

		public List<PanelHero> heroPanels;

		public ButtonUpgradeAnim buttonOpenNewHeroPanel;

		public GameButton buttonBuyRandomHero;

		private Vector2 heroPanelFirstPos;

		private Vector2 heroPanelDeltaPos;

		public RectTransform autoSkillParent;

		public Text textAutoSkillDesc;

		public ButtonOnOff autoSkillDistributionToggle;
	}
}
