using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class VersionInfoLabel : MonoBehaviour
	{
		public RectTransform rectTransform
		{
			get
			{
				RectTransform result;
				if ((result = this.m_rectTransform) == null)
				{
					result = (this.m_rectTransform = base.GetComponent<RectTransform>());
				}
				return result;
			}
		}

		public void InitStrings()
		{
			this.button.text.text = LM.Get("UI_PATCH_NOTES");
		}

		public void SetButtonOlderPatch()
		{
			Image component = this.button.GetComponent<Image>();
			component.sprite = this.spriteOlder;
			this.button.text.color = this.textButtonColorOlder;
			Shadow component2 = this.button.text.GetComponent<Shadow>();
			component2.enabled = false;
		}

		public void SetButtonNormalPatch()
		{
			Image component = this.button.GetComponent<Image>();
			component.sprite = this.spriteNormal;
			this.button.text.color = this.textButtonColorNormal;
			Shadow component2 = this.button.text.GetComponent<Shadow>();
			component2.enabled = true;
		}

		public Text textHeader;

		public Text textLabel;

		public GameButton button;

		public Sprite spriteOlder;

		public Sprite spriteNormal;

		public Color textButtonColorOlder;

		public Color textButtonColorNormal;

		private RectTransform m_rectTransform;
	}
}
