using System;

namespace Ui
{
	public class XiaomiWarningPopup : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
		}

		public override void Init()
		{
			this.InitStrings();
		}

		public void InitStrings()
		{
			this.okayButton.text.text = LM.Get("UI_OKAY");
		}

		public UiState oldState;

		public GameButton okayButton;
	}
}
