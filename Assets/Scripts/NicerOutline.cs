using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Effects/Extensions/Nicer Outline")]
public class NicerOutline : BaseMeshEffect
{
	public Color effectColor
	{
		get
		{
			return this.m_EffectColor;
		}
		set
		{
			this.m_EffectColor = value;
			if (base.graphic != null)
			{
				base.graphic.SetVerticesDirty();
			}
		}
	}

	public Vector2 effectDistance
	{
		get
		{
			return this.m_EffectDistance;
		}
		set
		{
			if (value.x > 600f)
			{
				value.x = 600f;
			}
			if (value.x < -600f)
			{
				value.x = -600f;
			}
			if (value.y > 600f)
			{
				value.y = 600f;
			}
			if (value.y < -600f)
			{
				value.y = -600f;
			}
			if (this.m_EffectDistance == value)
			{
				return;
			}
			this.m_EffectDistance = value;
			if (base.graphic != null)
			{
				base.graphic.SetVerticesDirty();
			}
		}
	}

	public bool useGraphicAlpha
	{
		get
		{
			return this.m_UseGraphicAlpha;
		}
		set
		{
			this.m_UseGraphicAlpha = value;
			if (base.graphic != null)
			{
				base.graphic.SetVerticesDirty();
			}
		}
	}

	protected void ApplyShadowZeroAlloc(List<UIVertex> verts, Color32 color, int start, int end, float x, float y)
	{
		int num = verts.Count * 2;
		if (verts.Capacity < num)
		{
			verts.Capacity = num;
		}
		for (int i = start; i < end; i++)
		{
			UIVertex uivertex = verts[i];
			verts.Add(uivertex);
			Vector3 position = uivertex.position;
			position.x += x;
			position.y += y;
			uivertex.position = position;
			Color32 color2 = color;
			if (this.m_UseGraphicAlpha)
			{
				color2.a = (byte)(color2.a * verts[i].color.a / byte.MaxValue);
			}
			uivertex.color = color2;
			verts[i] = uivertex;
		}
	}

	protected void ApplyShadow(List<UIVertex> verts, Color32 color, int start, int end, float x, float y)
	{
		int num = verts.Count * 2;
		if (verts.Capacity < num)
		{
			verts.Capacity = num;
		}
		this.ApplyShadowZeroAlloc(verts, color, start, end, x, y);
	}

	public override void ModifyMesh(VertexHelper vh)
	{
		if (!this.IsActive())
		{
			return;
		}
		List<UIVertex> list = new List<UIVertex>();
		vh.GetUIVertexStream(list);
		Text component = base.GetComponent<Text>();
		float num = 1f;
		if (component && component.resizeTextForBestFit)
		{
			num = (float)component.cachedTextGenerator.fontSizeUsedForBestFit / (float)(component.resizeTextMaxSize - 1);
		}
		float num2 = this.effectDistance.x * num;
		float num3 = this.effectDistance.y * num;
		int start = 0;
		int count = list.Count;
		this.ApplyShadow(list, this.effectColor, start, list.Count, num2, num3);
		start = count;
		count = list.Count;
		this.ApplyShadow(list, this.effectColor, start, list.Count, num2, -num3);
		start = count;
		count = list.Count;
		this.ApplyShadow(list, this.effectColor, start, list.Count, -num2, num3);
		start = count;
		count = list.Count;
		this.ApplyShadow(list, this.effectColor, start, list.Count, -num2, -num3);
		start = count;
		count = list.Count;
		this.ApplyShadow(list, this.effectColor, start, list.Count, num2, 0f);
		start = count;
		count = list.Count;
		this.ApplyShadow(list, this.effectColor, start, list.Count, -num2, 0f);
		start = count;
		count = list.Count;
		this.ApplyShadow(list, this.effectColor, start, list.Count, 0f, num3);
		start = count;
		count = list.Count;
		this.ApplyShadow(list, this.effectColor, start, list.Count, 0f, -num3);
		vh.Clear();
		vh.AddUIVertexTriangleStream(list);
	}

	[SerializeField]
	private Color m_EffectColor = new Color(0f, 0f, 0f, 0.5f);

	[SerializeField]
	private Vector2 m_EffectDistance = new Vector2(1f, -1f);

	[SerializeField]
	private bool m_UseGraphicAlpha = true;
}
