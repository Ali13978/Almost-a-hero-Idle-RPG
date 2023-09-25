using System;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelGearScreen : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
			if (this.doTransitionBetweenHeroes)
			{
				base.AddToUpdates();
			}
		}

		public override void Init()
		{
			this.InitStrings();
			if (this.doTransitionBetweenHeroes)
			{
				this.heroTransitionAnimation = new HeroTransitionAnimation(this.heroAnimation, this);
			}
		}

		public override void AahUpdate(float dt)
		{
			this.heroTransitionAnimation.Update();
		}

		public void OnClose()
		{
			if (this.doTransitionBetweenHeroes)
			{
				this.heroTransitionAnimation.OnClose();
			}
			else
			{
				this.heroAnimation.OnClose();
			}
		}

		public void InitStrings()
		{
			int i = 0;
			int num = this.panelGears.Length;
			while (i < num)
			{
				this.panelGears[i].buttonUpgrade.textDown.text = LM.Get("UI_UPGRADE");
				i++;
			}
			this.textShopBg.text = LM.Get("UI_GEAR_SHOP_DESC");
			this.buttonShop.text.text = LM.Get("UI_GO_TO_SHOP");
			if (this.buttonTab != null)
			{
				this.buttonTab.text.text = LM.Get("UI_TAB_GEAR");
			}
		}

		public void SetDetails(string heroName, int evolveLevel, int skinIndex)
		{
			this.evolveStars.SetNumberOfStars(evolveLevel, 6);
			if (this.doTransitionBetweenHeroes)
			{
				this.heroTransitionAnimation.SetHeroAnimation(heroName, skinIndex, false, evolveLevel);
			}
			else
			{
				this.InitHeroBase(evolveLevel);
			}
		}

		public void InitHeroBase(int evolveLevel)
		{
			this.heroPentagram.SetSprite(evolveLevel);
			if (this.oldEvolveLevel != evolveLevel && this.spineAnimCharBase.AnimationState != null)
			{
				this.oldEvolveLevel = evolveLevel;
				if (evolveLevel > 0)
				{
					this.spineAnimCharBase.gameObject.SetActive(true);
					this.spineAnimCharBase.Skeleton.SetSkin(PanelGearScreen.evolveSkins[evolveLevel - 1]);
					this.spineAnimCharBase.AnimationState.SetAnimation(0, "animation", true);
				}
				else
				{
					this.spineAnimCharBase.gameObject.SetActive(false);
				}
			}
			if (this.heroTransitionFX != null)
			{
				this.heroTransitionFX.color = this.heroTransitionFXColorsPerLevel[evolveLevel];
			}
		}

		public static string[] evolveSkins = new string[]
		{
			"common",
			"uncommon",
			"rare",
			"epic",
			"legendary",
			"mythical"
		};

		private int oldEvolveLevel = -1;

		public HeroAnimation heroAnimation;

		public PanelGear[] panelGears;

		public ButtonUpgradeAnim buttonEvolve;

		public EvolveStars evolveStars;

		public Text textEvolveDesc;

		public Image imageMaxEvolve;

		public SkeletonGraphic spineAnimCharBase;

		public GameButton buttonTab;

		public Image imageShopBg;

		public Text textShopBg;

		public GameButton buttonShop;

		public GameButton buttonSkin;

		public HeroPentagram heroPentagram;

		public NotificationBadge skinsNotificationBadge;

		public RectTransform heroBasePivotTransform;

		public RectTransform heroBaseAnimationPivotTransform;

		public bool doTransitionBetweenHeroes;

		public SkeletonGraphic heroTransitionFX;

		public Color[] heroTransitionFXColorsPerLevel;

		private bool callAppearAnimationWhenHeroLoaded;

		private bool waitForDisappearAnim;

		private bool rewinding;

		private int loadingHeroLevel;

		private HeroTransitionAnimation heroTransitionAnimation;
	}
}
