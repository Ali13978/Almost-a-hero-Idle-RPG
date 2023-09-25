using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObbPermissionDialog : MonoBehaviour
{
	public void Ask(Action onOkay)
	{
		this.Ask(onOkay, null);
	}

	public void Ask(Action onAccept, Action onReject)
	{
		base.gameObject.SetActive(true);
		this.acceptButton.onClick.RemoveAllListeners();
		this.acceptButton.onClick.AddListener(delegate()
		{
			onAccept();
			this.gameObject.SetActive(false);
		});
		if (onReject == null)
		{
			this.rejectButton.gameObject.SetActive(false);
			RectTransform component = this.acceptButton.GetComponent<RectTransform>();
			component.SetAnchorPosX(0f);
			component.SetSizeDeltaX(536f);
		}
		else
		{
			this.rejectButton.gameObject.SetActive(true);
			RectTransform component2 = this.acceptButton.GetComponent<RectTransform>();
			component2.SetAnchorPosX(146f);
			component2.SetSizeDeltaX(293f);
			this.rejectButton.onClick.RemoveAllListeners();
			this.rejectButton.onClick.AddListener(delegate()
			{
				onReject();
				this.gameObject.SetActive(false);
			});
		}
	}

	public Text description;

	public Text buttonText;

	public Button acceptButton;

	public Button rejectButton;

	public EventSystem eventSystem;
}
