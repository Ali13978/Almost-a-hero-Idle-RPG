using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[AddComponentMenu("Layout/Limited Content Size Fitter", 141)]
[ExecuteInEditMode]
[RequireComponent(typeof(RectTransform))]
public class LimittedContentSizeFitter : UIBehaviour, ILayoutSelfController, ILayoutController
{
	protected LimittedContentSizeFitter()
	{
	}

	private RectTransform rectTransform
	{
		get
		{
			if (this.m_Rect == null)
			{
				this.m_Rect = base.GetComponent<RectTransform>();
			}
			return this.m_Rect;
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.SetDirty();
	}

	protected override void OnDisable()
	{
		this.m_Tracker.Clear();
		LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
		base.OnDisable();
	}

	protected override void OnRectTransformDimensionsChange()
	{
		this.SetDirty();
	}

	private void HandleSelfFittingAlongAxis(int axis)
	{
		LimittedContentSizeFitter.FitMode fitMode = (axis != 0) ? this.verticalFit : this.horizontalFit;
		if (fitMode == LimittedContentSizeFitter.FitMode.Unconstrained)
		{
			this.m_Tracker.Add(this, this.rectTransform, DrivenTransformProperties.None);
			return;
		}
		this.m_Tracker.Add(this, this.rectTransform, (axis != 0) ? DrivenTransformProperties.SizeDeltaY : DrivenTransformProperties.SizeDeltaX);
		if (fitMode == LimittedContentSizeFitter.FitMode.MinSize)
		{
			this.rectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis, LayoutUtility.GetMinSize(this.m_Rect, axis));
		}
		else
		{
			float preferredSize = LayoutUtility.GetPreferredSize(this.m_Rect, axis);
			if (this.maxWidth > 0f && this.maxWidth < preferredSize)
			{
				preferredSize = this.maxWidth;
			}
			this.rectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis, preferredSize);
		}
	}

	public virtual void SetLayoutHorizontal()
	{
		this.m_Tracker.Clear();
		this.HandleSelfFittingAlongAxis(0);
	}

	public virtual void SetLayoutVertical()
	{
		this.HandleSelfFittingAlongAxis(1);
	}

	protected void SetDirty()
	{
		if (!this.IsActive())
		{
			return;
		}
		LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
	}

	public LimittedContentSizeFitter.FitMode horizontalFit;

	public LimittedContentSizeFitter.FitMode verticalFit;

	public float maxWidth = -1f;

	[NonSerialized]
	private RectTransform m_Rect;

	private DrivenRectTransformTracker m_Tracker;

	public enum FitMode
	{
		Unconstrained,
		MinSize,
		PreferredSize
	}
}
