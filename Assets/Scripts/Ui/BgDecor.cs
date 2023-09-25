using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class BgDecor : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToUpdates();
		}

		public void SetSprites(bool isDecorsActive, int tabIndex, bool isFullscreen, bool shouldHideDecorUp)
		{
			this.bg.sprite = this.spritesBg[tabIndex];
			if (isDecorsActive)
			{
				foreach (Image image in this.decorCorners)
				{
					image.gameObject.SetActive(true);
					image.sprite = this.spritesCorner[tabIndex];
				}
				foreach (Image image2 in this.decorLines)
				{
					image2.gameObject.SetActive(true);
					image2.sprite = this.spritesLine[tabIndex];
				}
				foreach (GameObject gameObject in this.decorsUp)
				{
					gameObject.SetActive(!shouldHideDecorUp);
				}
			}
			else
			{
				foreach (Image image3 in this.decorCorners)
				{
					image3.gameObject.SetActive(false);
				}
				foreach (Image image4 in this.decorLines)
				{
					image4.gameObject.SetActive(false);
				}
			}
			float y = 1110f;
			if (isFullscreen)
			{
				y = 1260f;
			}
			this.bgDecorMask.sizeDelta = new Vector2(this.bgDecorMask.sizeDelta.x, y);
		}

		public override void AahUpdate(float dt)
		{
		}

		public RectTransform rectTransform;

		public Image[] decorCorners;

		public Image[] decorLines;

		public Image bg;

		public Sprite[] spritesCorner;

		public GameObject[] decorsUp;

		public Sprite[] spritesLine;

		public Sprite[] spritesBg;

		public RectTransform scrollViewContent;

		public RectTransform bgDecorMask;
	}
}
