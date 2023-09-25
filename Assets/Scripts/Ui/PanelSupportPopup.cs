using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelSupportPopup : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToUpdates();
		}

		public override void AahUpdate(float dt)
		{
			this.bTimer += dt;
		}

		public void InitStrings()
		{
			this.header.text = LM.Get("UI_OPTIONS_SUPPORT");
			this.description.text = LM.Get("UI_OPTIONS_SUPPORT_DESC");
			this.reportBug.text.text = LM.Get("UI_OPTIONS_SUPPORT_BUG");
			this.reportPaymentIssue.text.text = LM.Get("UI_OPTIONS_SUPPORT_PAYMENT");
			this.feedback.text.text = LM.Get("UI_OPTIONS_SUPPORT_FEEDBACK");
		}

		public Text header;

		public Text description;

		public GameButton reportBug;

		public GameButton reportPaymentIssue;

		public GameButton feedback;

		public GameButton close;

		public RectTransform popupRect;

		[NonSerialized]
		public float bTimer = 2f;
	}
}
