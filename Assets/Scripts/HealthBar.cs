using System;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
	public void SetScale(float scale)
	{
		this.scale = ((!float.IsNaN(scale)) ? scale : 0f);
		Vector3 localScale = this.scaled.localScale;
		localScale.x = this.scale;
		this.scaled.localScale = localScale;
	}

	public void SetColor(Color color, Color blackCurtainColor)
	{
		this.on.color = color * blackCurtainColor;
		this.background.color = blackCurtainColor;
		this.foreground.color = this.foregroundColor * blackCurtainColor;
	}

	private void Start()
	{
		this.foregroundColor = this.foreground.color;
	}

	[NonSerialized]
	public float scale;

	[SerializeField]
	private Transform scaled;

	[SerializeField]
	private SpriteRenderer on;

	[SerializeField]
	private SpriteRenderer background;

	[SerializeField]
	private SpriteRenderer foreground;

	private Color foregroundColor = new Color(0.8627f, 0.2235f, 0.1216f, 1f);
}
