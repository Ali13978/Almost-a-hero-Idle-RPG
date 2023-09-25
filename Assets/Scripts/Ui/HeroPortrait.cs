using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class HeroPortrait : AahMonoBehaviour
	{
		public void SetHero(Sprite spriteHero, int evolveLevel, bool setNativeSizePortrait = true, float bgShift = -5f, bool shiftHeroPortrait = false)
		{
			if (evolveLevel == -1)
			{
				this.hero.sprite = spriteHero;
				this.hero.rectTransform.SetAnchorPosY(0f);
				this.hero.rectTransform.sizeDelta = HeroPortrait.LockedHeroPortraitSize;
				this.bg.sprite = this.heroLockedBackground;
				this.bg.color = HeroPortrait.White;
				this.bg.rectTransform.sizeDelta = HeroPortrait.LockedHeroBackgroundSize;
				this.bg.rectTransform.SetAnchorPosY(this.frame.rectTransform.anchoredPosition.y);
				this.frame.gameObject.SetActive(false);
			}
			else
			{
				this.hero.sprite = spriteHero;
				if (shiftHeroPortrait)
				{
					this.hero.rectTransform.SetAnchorPosY(-9f);
				}
				this.frame.gameObject.SetActive(true);
				this.bg.sprite = this.spritesHeroLevelBg[evolveLevel];
				this.bg.color = ((evolveLevel != 0) ? HeroPortrait.White : this.DefaultBackgroundColor);
				this.frame.sprite = this.spritesHeroLevelFrame[evolveLevel];
				this.bg.rectTransform.SetSizeDeltaY(this.bg.rectTransform.sizeDelta.y + bgShift);
				this.bg.rectTransform.SetAnchorPosY(this.frame.rectTransform.anchoredPosition.y + bgShift);
				this.bg.rectTransform.sizeDelta = ((evolveLevel != 0) ? HeroPortrait.HeroBackgroundSize : HeroPortrait.HeroDefaultBackgroundSize);
				if (setNativeSizePortrait)
				{
					this.hero.SetNativeSize();
				}
				this.frame.SetNativeSize();
			}
		}

		public Image bg;

		public Image hero;

		public Image frame;

		public Sprite heroLockedBackground;

		public Sprite[] spritesHeroLevelBg;

		public Sprite[] spritesHeroLevelFrame;

		public Color DefaultBackgroundColor;

		private static readonly Vector2 LockedHeroBackgroundSize = new Vector2(140.5f, 170f);

		private static readonly Vector2 HeroDefaultBackgroundSize = new Vector2(120f, 124.6f);

		private static readonly Vector2 HeroBackgroundSize = new Vector2(130f, 124.6f);

		private static readonly Vector2 LockedHeroPortraitSize = new Vector2(135.7f, 152.95f);

		private static readonly Color White = Color.white;
	}
}
