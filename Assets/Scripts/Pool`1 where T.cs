using System;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : Component
{
	public Pool(T prefab, int initialCapacity = 16, int max = 2147483647)
	{
		this.freeObjects = new Stack<T>(initialCapacity);
		this.max = max;
		this.prefab = prefab;
	}

	public int Count
	{
		get
		{
			return this.freeObjects.Count;
		}
	}

	public int Peak { get; private set; }

	public T Obtain()
	{
		T result;
		if (this.freeObjects.Count == 0)
		{
			result = UnityEngine.Object.Instantiate<T>(this.prefab, this.prefab.transform.parent);
		}
		else
		{
			result = this.freeObjects.Pop();
		}
		result.gameObject.SetActive(true);
		return result;
	}

	public void Free(T obj)
	{
		if (obj == null)
		{
			throw new ArgumentNullException("obj", "obj cannot be null");
		}
		if (this.freeObjects.Count < this.max)
		{
			this.freeObjects.Push(obj);
			obj.gameObject.SetActive(false);
			this.Peak = Math.Max(this.Peak, this.freeObjects.Count);
		}
		this.Reset(obj);
	}

	public void Clear()
	{
		this.freeObjects.Clear();
	}

	protected void Reset(T obj)
	{
		Pool<T>.IPoolable poolable = obj as Pool<T>.IPoolable;
		if (poolable != null)
		{
			poolable.Reset();
		}
	}

	public readonly int max;

	private readonly Stack<T> freeObjects;

	private T prefab;

	public interface IPoolable
	{
		void Reset();
	}
}
