using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class ButtonArtifactSlotUnlock : MonoBehaviour
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

		public void SetAsNormal()
		{
			this.background.rectTransform.SetSizeDeltaY(134f);
			this.currencyPlateNormal.gameObject.SetActive(true);
			this.currencyPlateBuying.gameObject.SetActive(false);
			this.buttonUpgrade.textUp.rectTransform.SetAnchorPosY(-157.8f);
			this.buttonUpgrade.textDown.gameObject.SetActive(false);
		}

		public void SetAsBuying()
		{
			this.background.rectTransform.SetSizeDeltaY(198f);
			this.currencyPlateNormal.gameObject.SetActive(false);
			this.currencyPlateBuying.gameObject.SetActive(true);
			this.buttonUpgrade.textUp.rectTransform.SetAnchorPosY(-137f);
			this.buttonUpgrade.textDown.gameObject.SetActive(true);
		}

		public Image background;

		public Image currencyBackground;

		public Image currencyPlateNormal;

		public Image currencyPlateBuying;

		public ButtonUpgradeAnim buttonUpgrade;

		private RectTransform m_rectTransform;
	}
}
