using System;
using UnityEngine;

public class ColorSetter : MonoBehaviour
{
	public void SetColor(Color color)
	{
		this.on.GetComponent<SpriteRenderer>().color = color;
	}

	[SerializeField]
	private GameObject on;
}
