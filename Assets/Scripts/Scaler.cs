using System;
using UnityEngine;

public class Scaler : MonoBehaviour
{
	public void SetScale(float scale)
	{
		if (float.IsNaN(scale))
		{
			this.scale = 0f;
		}
		else
		{
			this.scale = scale;
		}
		Vector3 localScale = this.scaled.transform.localScale;
		localScale.x = this.scale;
		this.scaled.transform.localScale = localScale;
	}

	public float GetScale()
	{
		return this.scaled.transform.localScale.x;
	}

	public float scale;

	public GameObject scaled;

	public SpriteRenderer coloredBar;

	public static Color colorShield = Utility.HexColor("18d3df");

	public static Color colorTime = Utility.HexColor("ffcb4a");
}
