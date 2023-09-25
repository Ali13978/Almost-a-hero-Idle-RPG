using System;
using SA.Common.Animation;
using UnityEngine;
using UnityEngine.UI;

public class SA_InApps_EditorUIController : MonoBehaviour
{
	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		Canvas component = base.GetComponent<Canvas>();
		component.sortingOrder = 10003;
	}

	public void ShowInAppPopup(string title, string describtion, string price, Action<bool> OnComplete = null)
	{
		if (this._CurrentTween != null)
		{
			this._CurrentTween.Stop();
		}
		if (this._FaderTween != null)
		{
			this._FaderTween.Stop();
		}
		this._OnComplete = OnComplete;
		this.Title.text = title;
		this.Describtion.text = describtion;
		this.Price.text = price;
		this.Animate(150f, -300f, EaseType.easeOutBack);
		this._CurrentTween.OnComplete += this.HandleOnInTweenComplete;
		this.FadeIn();
	}

	public void Close()
	{
		if (this._CurrentTween != null)
		{
			this._CurrentTween.Stop();
		}
		if (this._FaderTween != null)
		{
			this._FaderTween.Stop();
		}
		this.Animate(-300f, 150f, EaseType.easeInBack);
		this._CurrentTween.OnComplete += this.HandleOnOutTweenComplete;
		this.FadeOut();
		if (this._OnComplete != null)
		{
			this._OnComplete(this.IsSuccsesPurchase.isOn);
			this._OnComplete = null;
		}
	}

	private void HandleOnInTweenComplete()
	{
		this._CurrentTween = null;
	}

	private void HandleOnOutTweenComplete()
	{
		this._CurrentTween = null;
	}

	private void HandleOnValueChanged(float pos)
	{
		this.HightDependence.InitialRect.y = pos;
	}

	private void HandleOnFadeValueChanged(float a)
	{
		this.Fader.color = new Color(this.Fader.color.r, this.Fader.color.g, this.Fader.color.b, a);
	}

	private void HandleFadeComplete()
	{
		this.Fader.enabled = false;
	}

	private void FadeIn()
	{
		this.Fader.enabled = true;
		this._FaderTween = ValuesTween.Create();
		this._FaderTween.OnValueChanged += this.HandleOnFadeValueChanged;
		this._FaderTween.ValueTo(0f, 0.5f, 0.5f, EaseType.linear);
	}

	private void FadeOut()
	{
		this._FaderTween = ValuesTween.Create();
		this._FaderTween.OnValueChanged += this.HandleOnFadeValueChanged;
		this._FaderTween.OnComplete += this.HandleFadeComplete;
		this._FaderTween.ValueTo(0.5f, 0f, 0.5f, EaseType.linear);
	}

	private void Animate(float from, float to, EaseType easeType)
	{
		this._CurrentTween = ValuesTween.Create();
		this._CurrentTween.OnValueChanged += this.HandleOnValueChanged;
		this._CurrentTween.ValueTo(from, to, 0.5f, easeType);
	}

	public Text Title;

	public Text Describtion;

	public Text Price;

	public Toggle IsSuccsesPurchase;

	public Image Fader;

	public SA_UIHightDependence HightDependence;

	private ValuesTween _CurrentTween;

	private ValuesTween _FaderTween;

	private Action<bool> _OnComplete;
}
