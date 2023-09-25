using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class ButtonStateChange : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			if (this.text != null)
			{
				this.textActivePos = this.text.rectTransform.anchoredPosition.y;
			}
		}

		public void ActivateAnim()
		{
			if (this.spriteActive != null)
			{
				this.gameButton.raycastTarget.sprite = this.spriteActive;
			}
			if (this.allowColorChange)
			{
				this.text.color = this.colorTextActive;
			}
			if (this.textInactiveOffset != 0f)
			{
				this.text.rectTransform.anchoredPosition = new Vector2(this.text.rectTransform.anchoredPosition.x, this.textActivePos);
			}
		}

		public void DeactivateAnim()
		{
			if (this.spriteNotActive != null)
			{
				this.gameButton.raycastTarget.sprite = this.spriteNotActive;
			}
			if (this.allowColorChange)
			{
				this.text.color = this.colorTextNotActive;
			}
			if (this.textInactiveOffset != 0f)
			{
				this.text.rectTransform.anchoredPosition = new Vector2(this.text.rectTransform.anchoredPosition.x, this.textActivePos + this.textInactiveOffset);
			}
		}

		private UiManager uiManager;

		public UiState state;

		public bool usePanelTransition;

		public GameButton gameButton;

		[SerializeField]
		private Sprite spriteActive;

		[SerializeField]
		private Sprite spriteNotActive;

		public Text text;

		[SerializeField]
		private bool allowColorChange;

		[SerializeField]
		private Color colorTextActive;

		[SerializeField]
		private Color colorTextNotActive;

		[SerializeField]
		private float textInactiveOffset;

		private float textActivePos;

		public ButtonStateChange.EventCallType eventCallType;

		public enum EventCallType
		{
			OnClick,
			OnDown
		}
	}
}
