using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class PanelTrinketEffect : AahMonoBehaviour
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

		public static Color colorUpgrading = Utility.HexColor("646f19");

		public static Color colorNormal = Utility.HexColor("2B1B0D");

		public static Color colorLevelBgNormal = Utility.HexColor("53412C");

		public static Color colorLevelBgUpgrading = Utility.HexColor("8F9C2B");

		public RectTransform basicImagesParent;

		public Image background;

		public Image imageMaxed;

		public Image[] basicImages;

		public Image otherImage;

		public Image selectionFrame;

		public Image nonExisting;

		public Text textHeader;

		public Text textDesc;

		public Text textLevel;
	}
}
