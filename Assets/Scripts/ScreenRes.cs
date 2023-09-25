using System;
using UnityEngine;

public struct ScreenRes
{
	public static ScreenRes GetCurrentRes()
	{
		return new ScreenRes
		{
			width = Screen.width,
			height = Screen.height
		};
	}

	public static ScreenRes CreateWithAspectRatio(float ratio, int width)
	{
		return new ScreenRes
		{
			width = width,
			height = Mathf.RoundToInt((float)width * ratio)
		};
	}

	public float AspectRatio()
	{
		return (float)this.width / (float)this.height;
	}

	public override bool Equals(object obj)
	{
		return obj is ScreenRes && this == (ScreenRes)obj;
	}

	public override int GetHashCode()
	{
		return this.width.GetHashCode() ^ this.height.GetHashCode();
	}

	public static bool operator ==(ScreenRes x, ScreenRes y)
	{
		return x.width == y.width && x.height == y.height;
	}

	public static bool operator !=(ScreenRes x, ScreenRes y)
	{
		return x.width != y.width || x.height != y.height;
	}

	public int width;

	public int height;
}
