using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class ChristmasTreeOfferWidget : MonoBehaviour
	{
		public ButtonUpgradeAnim buyButton;

		public Image offerIcon;

		public Image priceBackground;

		public Image outlineImage;

		public GameObject dependencyLeft;

		public ChristmasTreeOfferWidget.DependencyObject dependencyUnlockedLeft;

		public GameObject dependencyRight;

		public ChristmasTreeOfferWidget.DependencyObject dependencyUnlockedRight;

		[Serializable]
		public class DependencyObject
		{
			public GameObject parent;

			public TreeLightWidget[] lights;
		}
	}
}
