using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class DynamicLoadObject<T> where T : UnityEngine.Object
{
	public bool isReady { get; private set; }

	public bool isLoading { get; private set; }

	public void LoadAsync(MonoBehaviour coroutiner)
	{
		coroutiner.StartCoroutine(this.AsyncLoader());
	}

	private IEnumerator AsyncLoader()
	{
		if (this.isReady || this.isLoading)
		{
			yield break;
		}
		this.isLoading = true;
		int extensionIndex = this.path.LastIndexOf('.');
		ResourceRequest operation = Resources.LoadAsync<T>(this.path.Substring(0, extensionIndex));
		yield return operation;
		if (operation.isDone && operation.asset != null)
		{
			this.obj = (operation.asset as T);
			this.isReady = true;
		}
		this.isLoading = false;
		yield break;
	}

	public void LoadObject()
	{
		if (this.isReady)
		{
			return;
		}
		this.isLoading = true;
		int length = this.path.LastIndexOf('.');
		this.obj = Resources.Load<T>(this.path.Substring(0, length));
		if (this.obj != null)
		{
			this.isReady = true;
		}
		this.isLoading = false;
	}

	public T GetObject()
	{
		if (this.isReady)
		{
			this.lastUseTime = Time.unscaledTime;
			return this.obj;
		}
		UnityEngine.Debug.LogError("The object is not ready returning null");
		return (T)((object)null);
	}

	public T LoadAndGetObject()
	{
		if (this.isReady)
		{
			this.lastUseTime = Time.unscaledTime;
			return this.obj;
		}
		this.lastUseTime = Time.unscaledTime;
		this.LoadObject();
		return this.obj;
	}

	public void CheckLifetime()
	{
		if (Time.unscaledTime - this.lastUseTime > this.lifetime)
		{
			this.Unload();
		}
	}

	public void Unload()
	{
		Resources.UnloadAsset(this.obj);
		this.isReady = false;
		this.obj = (T)((object)null);
	}

	public void RequestUnloading()
	{
		if (this.lifetime > 5f)
		{
			this.lifetime = 5f;
		}
	}

	public string path;

	private T obj;

	private float lastUseTime;

	private float lifetime = 60f;
}
