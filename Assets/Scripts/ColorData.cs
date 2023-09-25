using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ColorData
{
	public override string ToString()
	{
		return string.Format("Name: {0}, Color: {1}", this.name, this.color.ToString());
	}

	public string name;

	public Color color;

	public bool isLocked;

	public List<string> tags;
}
