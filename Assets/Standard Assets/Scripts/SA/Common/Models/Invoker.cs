using System;
using UnityEngine;

namespace SA.Common.Models
{
	public class Invoker : MonoBehaviour
	{
		public static Invoker Create(string name)
		{
			return new GameObject("SA.Common.Models.Invoker: " + name).AddComponent<Invoker>();
		}

		private void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		public void StartInvoke(Action callback, float time)
		{
			this._callback = callback;
			base.Invoke("TimeOut", time);
		}

		private void TimeOut()
		{
			if (this._callback != null)
			{
				this._callback();
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}

		private Action _callback;
	}
}
