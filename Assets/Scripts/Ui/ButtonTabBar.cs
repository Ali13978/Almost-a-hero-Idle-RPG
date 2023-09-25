using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class ButtonTabBar : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
			base.AddToUpdates();
		}

		public override void Init()
		{
			this.image = base.GetComponent<Image>();
			this.spriteUp = this.image.sprite;
			this.spriteSymbolUp = this.imageSymbol.sprite;
			this.colorTextUp = this.text.color;
		}

		public void SetIconSprites(Sprite up, Sprite down)
		{
			if (this.spriteSymbolDown != down || this.spriteSymbolUp != up)
			{
				this.forceToNativeSize = true;
				this.state = ButtonTabBar.State.NONE;
			}
			this.spriteSymbolUp = up;
			this.spriteSymbolDown = down;
		}

		public override void AahUpdate(float dt)
		{
			if (!this.gameButton.interactable && this.state != ButtonTabBar.State.NOT_INTERACTABLE)
			{
				this.image.sprite = this.spriteLocked;
				this.imageSymbol.gameObject.SetActive(false);
				this.state = ButtonTabBar.State.NOT_INTERACTABLE;
			}
			else if (this.isDown && this.state != ButtonTabBar.State.PRESSED)
			{
				this.image.sprite = this.spriteDown;
				this.imageSymbol.gameObject.SetActive(true);
				this.imageSymbol.sprite = this.spriteSymbolDown;
				this.text.color = this.colorTextDown;
				this.state = ButtonTabBar.State.PRESSED;
			}
			else if (this.gameButton.interactable && !this.isDown && this.state != ButtonTabBar.State.INTERACTABLE)
			{
				this.image.sprite = this.spriteUp;
				this.imageSymbol.gameObject.SetActive(true);
				this.imageSymbol.sprite = this.spriteSymbolUp;
				this.text.color = this.colorTextUp;
				this.state = ButtonTabBar.State.INTERACTABLE;
			}
			if (this.forceToNativeSize)
			{
				this.imageSymbol.SetNativeSize();
				this.forceToNativeSize = false;
			}
		}

		public GameButton gameButton;

		private Image image;

		public Sprite spriteDown;

		private Sprite spriteUp;

		public Sprite spriteLocked;

		public Image imageSymbol;

		public Sprite spriteSymbolDown;

		private Sprite spriteSymbolUp;

		public Text text;

		public Color colorTextDown;

		private Color colorTextUp;

		public bool isDown;

		private bool forceToNativeSize;

		public NotificationBadge notificationBadge;

		public UiState uiState;

		private ButtonTabBar.State state;

		private enum State
		{
			NONE,
			INTERACTABLE,
			PRESSED,
			NOT_INTERACTABLE
		}
	}
}
