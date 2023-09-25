using System;
using SA.Common.Pattern;
using UnityEngine;

public class DisconnectButton : MonoBehaviour
{
	private void Start()
	{
		this.w = (float)Screen.width * 0.2f;
		this.h = (float)Screen.height * 0.1f;
		this.r.x = this.w * 0.1f;
		this.r.y = this.h * 0.1f;
		this.r.width = this.w;
		this.r.height = this.h;
	}

	private void OnGUI()
	{
		if (GUI.Button(this.r, "Disconnect"))
		{
			Singleton<GameCenter_RTM>.Instance.Disconnect();
		}
	}

	private float w;

	private float h;

	private Rect r = default(Rect);
}
