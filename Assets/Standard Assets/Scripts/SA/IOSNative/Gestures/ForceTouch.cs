using System;
using System.Diagnostics;
using SA.Common.Pattern;
using UnityEngine;

namespace SA.IOSNative.Gestures
{
	public class ForceTouch : Singleton<ForceTouch>
	{
		////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action OnForceTouchStarted;



		////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action OnForceTouchFinished;



		////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<ForceInfo> OnForceChanged;



		////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<string> OnAppShortcutClick;



		private void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		public void Setup(float forceTouchDelay, float baseForceTouchPressure, float triggeringForceTouchPressure)
		{
		}

		public static string AppOpenshortcutItem
		{
			get
			{
				return string.Empty;
			}
		}

		private void didStartForce(string array)
		{
			ForceTouch._IsTouchTrigerred = true;
			this.OnForceTouchStarted();
		}

		private void didForceChanged(string array)
		{
			if (!ForceTouch._IsTouchTrigerred)
			{
				return;
			}
			string[] array2 = array.Split(new char[]
			{
				'|'
			});
			float force = Convert.ToSingle(array2[0]);
			float maxForce = Convert.ToSingle(array2[1]);
			ForceInfo obj = new ForceInfo(force, maxForce);
			this.OnForceChanged(obj);
		}

		private void didForceEnded(string array)
		{
			ForceTouch._IsTouchTrigerred = false;
			this.OnForceTouchFinished();
		}

		private void performActionForShortcutItem(string action)
		{
			this.OnAppShortcutClick(action);
		}

		private static bool _IsTouchTrigerred;
	}
}
