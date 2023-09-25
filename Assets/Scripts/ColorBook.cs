using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorBookRepository", menuName = "Scriptable Objects/Color Book Repository")]
public class ColorBook : ScriptableObject
{
	public int Length
	{
		get
		{
			return this.colors.Count;
		}
	}

	public ColorData this[int index]
	{
		get
		{
			return this.colors[index];
		}
		set
		{
			this.colors[index] = value;
		}
	}

	public void AddPendingColor()
	{
		this.AddColor(this.pendingColorName, this.pendingColor);
	}

	public void RemoveColorAt(int index)
	{
		this.colors.RemoveAt(index);
	}

	public void AddColor(string colorName, Color colorSelf)
	{
		this.colors.Add(new ColorData
		{
			name = colorName,
			color = colorSelf,
			isLocked = true,
			tags = new List<string>()
		});
	}

	public void AddTag(int index, string tag)
	{
		this.colors[index].tags.Add(tag);
	}

	public bool HasColor(Color color, out ColorData match, out int index)
	{
		index = -1;
		foreach (ColorData colorData in this.colors)
		{
			index++;
			if (this.AlmosEqual(color.r, colorData.color.r) && this.AlmosEqual(color.g, colorData.color.g) && this.AlmosEqual(color.b, colorData.color.b) && this.AlmosEqual(color.a, colorData.color.a))
			{
				match = colorData;
				return true;
			}
		}
		match = null;
		return false;
	}

	private bool AlmosEqual(float x, float b)
	{
		return Mathf.Abs(x - b) <= 0.001f;
	}

	public bool isAllUnlocked;

	[SerializeField]
	private List<ColorData> colors;

	public string pendingColorName;

	public Color pendingColor;
}
