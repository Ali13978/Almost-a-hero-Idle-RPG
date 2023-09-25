using System;
using System.Diagnostics;
using SA.Common.Pattern;
using UnityEngine;

public class ISN_GestureRecognizer : Singleton<ISN_GestureRecognizer>
{
	////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action<ISN_SwipeDirection> OnSwipe;



	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	private void OnSwipeAction(string data)
	{
		int obj = Convert.ToInt32(data);
		this.OnSwipe((ISN_SwipeDirection)obj);
	}
}
