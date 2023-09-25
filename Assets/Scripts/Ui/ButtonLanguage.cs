using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class ButtonLanguage : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToUpdates();
		}

		public override void AahUpdate(float dt)
		{
			if (this.selected)
			{
				this.image.sprite = this.spriteSelected;
			}
			else
			{
				this.image.sprite = this.spriteNotSelected;
			}
		}

		public Sprite spriteNotSelected;

		public Sprite spriteSelected;

		public Image image;

		public GameButton gameButton;

		public bool selected;
	}
}
