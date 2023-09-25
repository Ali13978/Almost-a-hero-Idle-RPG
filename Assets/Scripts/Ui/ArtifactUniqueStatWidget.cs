using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class ArtifactUniqueStatWidget : MonoBehaviour
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

		public Text textStat;

		public Text readyToEvolveText;

		public CanvasGroup textCanvasGroup;

		public RectTransform textCanvasRect;

		public ButtonUpgradeAnim button;

		public RectTransform buttonParent;

		public Image imageLock;

		private RectTransform m_rectTransform;
	}
}
