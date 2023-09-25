using System;
using SA.Common.Animation;
using UnityEngine;
using UnityEngine.UI;

public class SA_Notifications_EditorUIController : MonoBehaviour
{
	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		SA_EditorTesting.CheckForEventSystem();
		Canvas component = base.GetComponent<Canvas>();
		component.sortingOrder = 10001;
	}

	public void ShowNotification(string title, string message, SA_EditorNotificationType type)
	{
		if (this._CurrentTween != null)
		{
			this._CurrentTween.Stop();
		}
		base.CancelInvoke("NotificationDelayComplete");
		this.Title.text = title;
		this.Message.text = message;
		foreach (Image image in this.Icons)
		{
			image.gameObject.SetActive(false);
		}
		switch (type)
		{
		case SA_EditorNotificationType.Message:
			this.Icons[3].gameObject.SetActive(true);
			break;
		case SA_EditorNotificationType.Achievement:
			this.Icons[0].gameObject.SetActive(true);
			break;
		case SA_EditorNotificationType.Leaderboards:
			this.Icons[2].gameObject.SetActive(true);
			break;
		case SA_EditorNotificationType.Error:
			this.Icons[1].gameObject.SetActive(true);
			break;
		}
		this.Animate(52f, -52f, EaseType.easeOutBack);
		this._CurrentTween.OnComplete += this.HandleOnInTweenComplete;
	}

	private void HandleOnInTweenComplete()
	{
		this._CurrentTween = null;
		base.Invoke("NotificationDelayComplete", 2f);
	}

	private void NotificationDelayComplete()
	{
		this.Animate(-52f, 58f, EaseType.easeInBack);
		this._CurrentTween.OnComplete += this.HandleOnOutTweenComplete;
	}

	private void HandleOnOutTweenComplete()
	{
		this._CurrentTween = null;
	}

	private void HandleOnValueChanged(float pos)
	{
		this.HightDependence.InitialRect.y = pos;
	}

	private void Animate(float from, float to, EaseType easeType)
	{
		this._CurrentTween = ValuesTween.Create();
		this._CurrentTween.OnValueChanged += this.HandleOnValueChanged;
		this._CurrentTween.ValueTo(from, to, 0.5f, easeType);
	}

	public Text Title;

	public Text Message;

	public Image[] Icons;

	public SA_UIHightDependence HightDependence;

	private ValuesTween _CurrentTween;
}
