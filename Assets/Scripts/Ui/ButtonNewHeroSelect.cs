using System;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class ButtonNewHeroSelect : AahMonoBehaviour
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

		public void SetButtonState(bool exists, bool buyable, bool unlocked, bool everSelected, int evolveLevel, Sprite spriteModeFlag = null)
		{
			this.gameButton.interactable = exists;
			if (!exists || !unlocked)
			{
				this.heroPortrait.SetHero(this.spriteLockedPortrait, -1, true, -5f, true);
				this.cornerIcon.gameObject.SetActive(false);
				this.unlockLevelLabel.gameObject.SetActive(true);
				this.lockIcon.gameObject.SetActive(true);
				this.heroPortrait.hero.color = ButtonNewHeroSelect.heroBuyableColor;
			}
			else
			{
				this.unlockLevelLabel.gameObject.SetActive(false);
				this.lockIcon.gameObject.SetActive(false);
				this.heroPortrait.SetHero(this.spriteHeroPortrait, evolveLevel, true, -5f, false);
				if (!buyable)
				{
					this.cornerIcon.sprite = spriteModeFlag;
					this.cornerIcon.SetNativeSize();
					this.cornerIcon.gameObject.SetActive(true);
					this.gameButton.text.enabled = false;
					this.heroPortrait.hero.color = ButtonNewHeroSelect.heroNotBuyableColor;
				}
				else if (!everSelected)
				{
					this.cornerIcon.sprite = this.spriteCornerNew;
					this.cornerIcon.SetNativeSize();
					this.cornerIcon.gameObject.SetActive(true);
					this.gameButton.text.enabled = true;
					this.heroPortrait.hero.color = ButtonNewHeroSelect.heroBuyableColor;
				}
				else
				{
					this.cornerIcon.sprite = spriteModeFlag;
					this.cornerIcon.SetNativeSize();
					this.cornerIcon.gameObject.SetActive(spriteModeFlag != null);
					this.heroPortrait.hero.color = ButtonNewHeroSelect.heroBuyableColor;
					this.gameButton.text.enabled = false;
				}
			}
		}

		private RectTransform m_rectTransform;

		public HeroPortrait heroPortrait;

		public GameButton gameButton;

		public Image cornerIcon;

		public Sprite spriteCornerNew;

		public Sprite spriteLockedPortrait;

		public Sprite spriteHeroPortrait;

		public Text unlockLevelLabel;

		public Image lockIcon;

		public HeroDataBase hero;

		private static readonly Color heroBuyableColor = Color.white;

		private static readonly Color heroNotBuyableColor = new Color(1f, 1f, 1f, 0.6f);
	}
}
