using System;
using UnityEngine;

namespace SA.Common.Pattern
{
	public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		public static T Instance
		{
			get
			{
				if (Singleton<T>.applicationIsQuitting)
				{
					return (T)((object)null);
				}
				if (Singleton<T>._instance == null)
				{
					Singleton<T>._instance = (UnityEngine.Object.FindObjectOfType(typeof(T)) as T);
					if (Singleton<T>._instance == null)
					{
						Singleton<T>._instance = new GameObject().AddComponent<T>();
						Singleton<T>._instance.gameObject.name = Singleton<T>._instance.GetType().FullName;
					}
				}
				return Singleton<T>._instance;
			}
		}

		public static bool HasInstance
		{
			get
			{
				return !Singleton<T>.IsDestroyed;
			}
		}

		public static bool IsDestroyed
		{
			get
			{
				return Singleton<T>._instance == null;
			}
		}

		protected virtual void OnDestroy()
		{
			Singleton<T>._instance = (T)((object)null);
			Singleton<T>.applicationIsQuitting = true;
		}

		protected virtual void OnApplicationQuit()
		{
			Singleton<T>._instance = (T)((object)null);
			Singleton<T>.applicationIsQuitting = true;
		}

		private static T _instance;

		private static bool applicationIsQuitting;
	}
}
