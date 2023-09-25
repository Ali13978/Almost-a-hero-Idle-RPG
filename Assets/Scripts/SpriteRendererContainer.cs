using System;
using UnityEngine;

public class SpriteRendererContainer
{
	public SpriteRendererContainer()
	{
		this.gameObject = new GameObject("SpriteRendererContainer");
		this.spriteRenderer = this.gameObject.AddComponent<SpriteRenderer>();
	}

	public GameObject gameObject;

	public SpriteRenderer spriteRenderer;
}
