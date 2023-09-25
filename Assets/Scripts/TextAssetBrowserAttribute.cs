using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class TextAssetBrowserAttribute : PropertyAttribute
{
	public TextAssetBrowserAttribute(string path)
	{
		this.path = path;
	}

	public string path;
}
