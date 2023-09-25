using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class StatWidget : MonoBehaviour
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

		public Image background;

		public Text statName;

		public Text statValue;

		public Text newTag;
	}
}
