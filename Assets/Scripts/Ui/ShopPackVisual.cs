using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class ShopPackVisual : MonoBehaviour
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

		public void DestoySelf()
		{
			UnityEngine.Object.Destroy(this.foregroundVisualParent.gameObject);
			UnityEngine.Object.Destroy(base.gameObject);
		}

		private RectTransform m_rectTransform;

		public int id;

		public int bacgroundIndex;

		public RectTransform foregroundVisualParent;

		public RectTransform backgroundVisualParent;

		public Text[] extraLabelComponents;
	}
}
