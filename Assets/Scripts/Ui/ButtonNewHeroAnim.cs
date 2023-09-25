using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class ButtonNewHeroAnim : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
			base.AddToUpdates();
		}

		public override void Init()
		{
			this.button = base.GetComponent<GameButton>();
		}

		public override void AahUpdate(float dt)
		{
			if (this.button.interactable)
			{
				this.textUp.color = this.textUpEnabledColor;
				this.textDown.color = this.textDownEnabledColor;
				this.coinIcon.color = Color.white;
			}
			else
			{
				this.textUp.color = this.textUpDisabledColor;
				this.textDown.color = this.textDownDisabledColor;
				this.coinIcon.color = this.coinDisabledColor;
			}
		}

		public Text textUp;

		public Text textDown;

		public GameButton gameButton;

		[SerializeField]
		private Image coinIcon;

		[SerializeField]
		private Color textUpEnabledColor;

		[SerializeField]
		private Color textDownEnabledColor;

		[SerializeField]
		private Color textUpDisabledColor;

		[SerializeField]
		private Color textDownDisabledColor;

		[SerializeField]
		private Color coinDisabledColor;

		private GameButton button;
	}
}
