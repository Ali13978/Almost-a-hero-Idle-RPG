using System;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactEffectLabel : MonoBehaviour
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

	public void DisableNewLabel()
	{
		this.newLabel.transform.parent.gameObject.SetActive(false);
	}

	public void EnableNewLabel()
	{
		this.newLabel.transform.parent.gameObject.SetActive(true);
	}

	public void DisableBackground()
	{
		this.background.gameObject.SetActive(false);
	}

	public void EnableBackground()
	{
		this.background.gameObject.SetActive(true);
	}

	private RectTransform m_rectTransform;

	public Text attributeDesc;

	public Text attributePercent;

	public Text newLabel;

	public Image background;
}
