using System;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(RectTransform))]
public class SA_UIWidthDependence : MonoBehaviour
{
	private void Awake()
	{
		if (Application.isPlaying)
		{
			this.ApplyTransformation();
		}
	}

	private void Update()
	{
		if (!Application.isPlaying)
		{
			if (!this.KeepRatioInEdiotr)
			{
				this.InitialRect = new Rect(this.rect.anchoredPosition.x, this.rect.anchoredPosition.y, this.rect.rect.width, this.rect.rect.height);
				this.InitialScreen = new Rect(0f, 0f, (float)Screen.width, (float)Screen.height);
				this.rect.hideFlags = HideFlags.None;
			}
			else
			{
				this.ApplyTransformation();
				this.rect.hideFlags = HideFlags.NotEditable;
			}
		}
		else if (!this.CaclulcateOnlyOntStart)
		{
			this.ApplyTransformation();
		}
	}

	public void ApplyTransformation()
	{
		float num = this.InitialScreen.width / this.InitialRect.width;
		float num2 = this.InitialRect.height / this.InitialRect.width;
		float num3 = (float)Screen.width / num;
		float num4 = num3 * num2;
		float num5 = this.InitialRect.y / this.InitialRect.height;
		float num6 = this.InitialRect.x / this.InitialRect.width;
		float y = num4 * num5;
		float x = num3 * num6;
		this.rect.anchoredPosition = new Vector2(x, y);
		this.rect.sizeDelta = new Vector2(num3, num4);
	}

	public RectTransform rect
	{
		get
		{
			if (this._rect == null)
			{
				this._rect = base.GetComponent<RectTransform>();
			}
			return this._rect;
		}
	}

	private void OnDetroy()
	{
		this.rect.hideFlags = HideFlags.None;
	}

	private RectTransform _rect;

	public bool KeepRatioInEdiotr;

	public bool CaclulcateOnlyOntStart;

	public Rect InitialRect = default(Rect);

	[HideInInspector]
	public Rect InitialScreen = default(Rect);
}
