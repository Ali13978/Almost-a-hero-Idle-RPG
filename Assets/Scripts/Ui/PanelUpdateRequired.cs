using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelUpdateRequired : AahMonoBehaviour
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
			this.textTitle.text = LM.Get("UI_UPDATE_REQ_HEADER");
			this.textDesc.text = LM.Get("UI_UPDATE_REQ_DESC");
			this.gameButton.text.text = LM.Get("UI_OKAY");
		}

		public CanvasGroup canvasGroup;

		public Text textTitle;

		public Text textDesc;

		public GameButton gameButton;

		public float timer;
	}
}
