using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class ArtifactAttributeLine : MonoBehaviour
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

		public Image background;

		public Image maxIndicator;

		public Text textName;

		public Text textAmount;

		private RectTransform m_rectTransform;
	}
}
