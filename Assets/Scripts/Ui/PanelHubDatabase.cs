using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelHubDatabase : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.InitStrings();
		}

		public void InitStrings()
		{
			this.textHeader.text = LM.Get("UI_HUB_DATABASE");
			this.buttonTabHeroesItems.text.text = LM.Get("UI_OFFER_ITEMS");
			this.buttonTabHeroesSkills.text.text = LM.Get("UI_TAB_SKILLS");
			this.buttonTabRings.text.text = LM.Get("UI_RINGS");
			if (this.buttonHeroes != null)
			{
				foreach (ButtonNewHeroSelect buttonNewHeroSelect in this.buttonHeroes)
				{
					buttonNewHeroSelect.gameButton.text.text = LM.Get("UI_NEW");
				}
			}
			this.panelSkills.InitStrings();
		}

		public void SetSelectedHero(int index)
		{
			this.selectedHero = index;
			int i = 0;
			int num = this.buttonHeroes.Length;
			while (i < num)
			{
				if (i == index)
				{
					this.buttonHeroes[i].transform.localScale = Vector3.one * 1.1f;
				}
				else
				{
					this.buttonHeroes[i].transform.localScale = Vector3.one * 1f;
				}
				i++;
			}
		}

		public RectTransform heroAvatarParent;

		public ButtonNewHeroSelect heroAvatarPrefab;

		public float heroAvatarButtonWidth;

		public float heroAvatarButtonInterval;

		public float heroAvatarButtonPadding;

		public ScrollRect heroesScrollRect;

		public ButtonSelectTotem[] buttonTotems;

		public PanelGearScreen panelGear;

		public PanelSkillsScreen panelSkills;

		public PanelHeroesRunes panelRunes;

		public PanelHero panelHero;

		public GameButton buttonTabHeroesItems;

		public GameButton buttonTabHeroesSkills;

		public GameButton buttonTabRings;

		public MenuShowCurrency menuShowCurrency;

		public GameButton buttonBack;

		public Text textHeader;

		public GameObject heroItemsParent;

		public GameObject heroUnlockHintParent;

		public Text heroUnlockHintLabel;

		[NonSerialized]
		public bool didSetScroll;

		[NonSerialized]
		public ButtonNewHeroSelect[] buttonHeroes;

		[NonSerialized]
		public int selectedHero;
	}
}
