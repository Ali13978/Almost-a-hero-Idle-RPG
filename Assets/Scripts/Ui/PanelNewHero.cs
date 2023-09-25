using System;
using System.Collections.Generic;
using Simulation;
using Static;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelNewHero : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
			base.AddToUpdates();
		}

		public override void Init()
		{
			this.InitStrings();
			this.heroTransitionAnimation = new HeroTransitionAnimation(this.heroAnimation);
		}

		public override void AahUpdate(float dt)
		{
			this.heroTransitionAnimation.Update();
		}

		public void SetScrollToZero()
		{
			this.scrollRect.horizontalNormalizedPosition = 0f;
		}

		public void InitStrings()
		{
			this.textTitle.text = LM.Get("UI_NEW_HERO_HEADER");
			this.buttonNewHeroBuy.textDown.text = LM.Get("UI_BUY");
			this.buttonSelectHero.text.text = LM.Get("UI_NEW_HERO_SELECT");
			if (this.initializedHeroButtons)
			{
				foreach (ButtonNewHeroSelect buttonNewHeroSelect in this.newHeroButtons)
				{
					buttonNewHeroSelect.gameButton.text.text = LM.Get("UI_NEW");
				}
			}
		}

		public void SetSelected()
		{
			for (int i = 0; i < this.newHeroButtons.Count; i++)
			{
				bool flag = i == this.selected;
				this.newHeroButtons[i].transform.localScale = Vector3.one * ((!flag) ? 1f : 1.1f);
			}
		}

		public void SelectHero(HeroDataBase hero, bool heroAvailable = true, bool onlyHint = false, GameMode? heroGameModeBusy = null)
		{
			this.buttonNewHeroBuy.gameObject.SetActive(false);
			this.buttonSelectHero.gameObject.SetActive(false);
			if (hero == null)
			{
				this.heroTransitionAnimation.OnClose();
				if (heroAvailable)
				{
					this.textNoHeroDesc.text = LM.Get("UI_NEW_HERO_DESC");
				}
				else
				{
					this.textNoHeroDesc.text = LM.Get("UI_NEW_HERO_NO_HEROES");
				}
				this.noHeroSelectedObject.SetActive(true);
				this.textHeroName.text = string.Empty;
				this.heroInfosParent.SetActive(false);
				this.objectButton.SetActive(false);
			}
			else
			{
				if (onlyHint)
				{
					this.heroTransitionAnimation.OnClose();
					this.noHeroSelectedObject.SetActive(true);
					this.heroInfosParent.SetActive(false);
					if (heroGameModeBusy == null)
					{
						HeroUnlockDescKey heroUnlockDescKey = this.uiManager.heroUnlockHintKeys[hero.GetId()];
						this.textNoHeroDesc.text = heroUnlockDescKey.GetStringDarkBrownAmount();
					}
					else
					{
						string arg = StringExtension.Concat("\n", AM.SizeText(AM.csdb(UiManager.GetModeName(heroGameModeBusy.Value)), 60), "\n");
						this.textNoHeroDesc.text = string.Format(LM.Get("HERO_BUSY_HINT"), arg);
					}
					this.objectButton.SetActive(false);
				}
				else
				{
					if (hero.randomSkinsEnabled && !this.shownHeroes.ContainsKey(hero.id))
					{
						this.shownHeroes.Add(hero.id, this.heroAnimation.lastSkinIndex);
					}
					this.noHeroSelectedObject.SetActive(false);
					this.textHeroName.text = LM.Get(hero.nameKey);
					this.imageHeroName.color = this.heroNameLevelColors[hero.evolveLevel];
					this.evolveStars.SetNumberOfStars(hero.evolveLevel, 6);
					this.textSelectedHeroDesc.text = LM.Get(hero.descKey);
					this.heroInfosParent.SetActive(true);
					this.objectButton.SetActive(true);
				}
				this.panelHeroClass.SetIcon(hero.heroClass);
			}
		}

		public int selected;

		public Text textTitle;

		public Text textHeroName;

		public Text textHeroType;

		public List<ButtonNewHeroSelect> newHeroButtons;

		public RectTransform scrollContent;

		public ScrollRect scrollRect;

		public GameObject objectButton;

		public ButtonUpgradeAnim buttonNewHeroBuy;

		public GameButton buttonSelectHero;

		public HeroAnimation heroAnimation;

		public HeroTransitionAnimation heroTransitionAnimation;

		public Image imageHeroName;

		private Color[] heroNameLevelColors = new Color[]
		{
			Utility.HexColor("B8995F"),
			Utility.HexColor("CC9547"),
			Utility.HexColor("5F9C00"),
			Utility.HexColor("0092DB"),
			Utility.HexColor("743B8B"),
			Utility.HexColor("FF8300"),
			Utility.HexColor("EF3C8D")
		};

		public EvolveStars evolveStars;

		public PanelHeroClass panelHeroClass;

		public Text textSelectedHeroDesc;

		public GameObject infoBg;

		public GameObject triangle;

		public GameObject noHeroSelectedObject;

		public GameObject heroInfosParent;

		public Text textNoHeroDesc;

		public GameButton[] buttonsClose;

		public GameObject inputBlocker;

		public GameObject duplicatedHeroWidget;

		public RectTransform popupRect;

		[NonSerialized]
		public UiManager uiManager;

		[NonSerialized]
		public bool initializedHeroButtons;

		[NonSerialized]
		public Dictionary<string, int> shownHeroes = new Dictionary<string, int>();
	}
}
