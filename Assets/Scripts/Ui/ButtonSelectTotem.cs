using System;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class ButtonSelectTotem : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToUpdates();
		}

		public override void AahUpdate(float dt)
		{
			if (this.locked)
			{
				if (this.image.sprite != this.spriteBgLocked)
				{
					this.image.sprite = this.spriteBgLocked;
					this.image.SetNativeSize();
				}
				this.imageIcon.gameObject.SetActive(false);
			}
			else
			{
				this.imageIcon.gameObject.SetActive(true);
				if (this.image.sprite != this.spriteBgUnlocked)
				{
					this.image.sprite = this.spriteBgUnlocked;
				}
				if (this.imageIcon.sprite != this.spriteIcon)
				{
					this.imageIcon.sprite = this.spriteIcon;
				}
			}
		}

		[SerializeField]
		private Image image;

		public Image imageIcon;

		public GameButton gameButton;

		public RectTransform rectTransform;

		public Image imageModeFlag;

		public Sprite spriteBgLocked;

		public Sprite spriteBgUnlocked;

		public Sprite spriteIcon;

		public bool locked;

		public TotemDataBase totem;
	}
}
