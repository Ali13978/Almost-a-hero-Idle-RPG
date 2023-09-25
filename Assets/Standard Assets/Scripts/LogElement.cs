using System;
using UnityEngine;
using UnityEngine.UI;

public class LogElement : MonoBehaviour
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

	public Text logText;

	public Text logTypetext;

	public Image logTypeImage;

	public Button button;

	public LogType logType;

	public bool isExpanded;

	public string condition;

	public string stackTrace;
}
