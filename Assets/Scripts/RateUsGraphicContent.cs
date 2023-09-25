using System;
using UnityEngine;
using UnityEngine.UI;

public class RateUsGraphicContent : MonoBehaviour
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

	public Image imageStars;

	private RectTransform m_rectTransform;
}
