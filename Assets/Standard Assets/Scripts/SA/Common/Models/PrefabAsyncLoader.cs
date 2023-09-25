using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SA.Common.Models
{
	public class PrefabAsyncLoader : MonoBehaviour
	{
		////[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<GameObject> ObjectLoadedAction;



		public static PrefabAsyncLoader Create()
		{
			return new GameObject("PrefabAsyncLoader").AddComponent<PrefabAsyncLoader>();
		}

		private void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		public void LoadAsync(string name)
		{
			this.PrefabPath = name;
			base.StartCoroutine(this.Load());
		}

		private IEnumerator Load()
		{
			ResourceRequest request = Resources.LoadAsync(this.PrefabPath);
			yield return request;
			if (request.asset == null)
			{
				UnityEngine.Debug.LogWarning("Prefab not found at path: " + this.PrefabPath);
				this.ObjectLoadedAction(null);
			}
			else
			{
				GameObject obj = UnityEngine.Object.Instantiate(request.asset) as GameObject;
				this.ObjectLoadedAction(obj);
			}
			UnityEngine.Object.Destroy(base.gameObject);
			yield break;
		}

		private string PrefabPath;
	}
}
