using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class LootPackTrinketCardAttribute : MonoBehaviour
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

		private RectTransform m_rectTransform;

		public Text label;
	}
}
