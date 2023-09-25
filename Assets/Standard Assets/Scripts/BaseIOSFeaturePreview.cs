using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseIOSFeaturePreview : MonoBehaviour
{
	protected virtual void InitStyles()
	{
		this.style = new GUIStyle();
		this.style.normal.textColor = Color.white;
		this.style.fontSize = 16;
		this.style.fontStyle = FontStyle.BoldAndItalic;
		this.style.alignment = TextAnchor.UpperLeft;
		this.style.wordWrap = true;
	}

	public virtual void Start()
	{
		this.InitStyles();
	}

	public void UpdateToStartPos()
	{
		this.StartY = this.YStartPos;
		this.StartX = this.XStartPos;
	}

	public void LoadLevel(string levelName)
	{
		SceneManager.LoadScene(levelName);
	}

	protected GUIStyle style;

	protected int buttonWidth = 200;

	protected int buttonHeight = 75;

	protected float StartY = 20f;

	protected float StartX = 10f;

	protected float XStartPos = 10f;

	protected float YStartPos = 10f;

	protected float XButtonStep = 220f;

	protected float YButtonStep = 90f;

	protected float YLableStep = 50f;
}
