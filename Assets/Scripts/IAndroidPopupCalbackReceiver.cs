using System;
using UnityEngine;

public interface IAndroidPopupCalbackReceiver
{
	void OnCallbackReceive(string message);

	Action onPositive { get; set; }

	Action onNegative { get; set; }

	Action onOkay { get; set; }

	GameObject gameObject { get; }
}
