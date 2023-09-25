using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class SkinInventoryButton : MonoBehaviour
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

		public Image skinIcon;

		public Image lockIcon;

		public Image frame;

		public Image equippedCheckmark;

		public GameObject newBadge;

		public Text newLabel;

		public GameButton gameButton;

		public Image priceTagIcon;

		public Image priceTagParent;

		public RectTransform allRenderersParent;

		private RectTransform m_rectTransform;
	}
}
