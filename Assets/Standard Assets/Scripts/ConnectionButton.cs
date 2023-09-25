using System;
using SA.Common.Pattern;
using UnityEngine;

public class ConnectionButton : MonoBehaviour
{
	private void Start()
	{
		this.w = (float)Screen.width * 0.2f;
		this.h = (float)Screen.height * 0.1f;
		this.r.x = ((float)Screen.width - this.w) / 2f;
		this.r.y = ((float)Screen.height - this.h) / 2f;
		this.r.width = this.w;
		this.r.height = this.h;
	}

	private void OnGUI()
	{
		if (GUI.Button(this.r, "Find Match"))
		{
			Singleton<GameCenter_RTM>.Instance.FindMatch(2, 2, string.Empty, null);
		}
	}

	private float w;

	private float h;

	private Rect r = default(Rect);
}
