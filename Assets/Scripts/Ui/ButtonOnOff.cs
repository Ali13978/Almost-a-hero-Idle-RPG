using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class ButtonOnOff : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
			base.AddToUpdates();
		}

		public override void Init()
		{
			if (this.gameButton.text != null)
			{
				this.onTextPos = this.gameButton.text.rectTransform.anchoredPosition.y;
				this.onTextColor = this.gameButton.text.color;
			}
			if (this.outline != null)
			{
				this.onOutlineOn = this.outline.enabled;
				this.onOutlineColor = this.outline.effectColor;
			}
			this.wasOn = this.isOn;
		}

		public override void AahUpdate(float dt)
		{
			this.gameButton.raycastTarget.sprite = ((!this.isOn) ? this.spriteOff : this.spriteOn);
			if (this.gameButton.text != null)
			{
				this.gameButton.text.text = ((!this.isOn) ? ButtonOnOff.offString : ButtonOnOff.onString);
				this.gameButton.text.rectTransform.anchoredPosition = new Vector2(this.gameButton.text.rectTransform.anchoredPosition.x, (!this.isOn) ? (this.onTextPos + this.offTextYOffset) : this.onTextPos);
				this.gameButton.text.color = ((!this.isOn) ? this.offTextColor : this.onTextColor);
			}
			if (this.outline != null)
			{
				this.outline.enabled = ((!this.isOn) ? this.offOutlineOn : this.onOutlineOn);
				this.outline.effectColor = ((!this.isOn) ? this.offOutlineColor : this.onOutlineColor);
			}
			if (this.isOn && !this.wasOn)
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiToggleOn, 1f));
			}
			else if (!this.isOn && this.wasOn)
			{
				UiManager.sounds.Add(new SoundEventUiSimple(SoundArchieve.inst.uiToggleOff, 1f));
			}
			this.wasOn = this.isOn;
		}

		public bool isOn;

		private bool wasOn;

		public GameButton gameButton;

		public Sprite spriteOn;

		public Sprite spriteOff;

		public static string onString;

		public static string offString;

		public float offTextYOffset;

		private float onTextPos;

		public Color offTextColor;

		private Color onTextColor;

		public Outline outline;

		public bool offOutlineOn = true;

		private bool onOutlineOn;

		public Color offOutlineColor;

		private Color onOutlineColor;
	}
}
