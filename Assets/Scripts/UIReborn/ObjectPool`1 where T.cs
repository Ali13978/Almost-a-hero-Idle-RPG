using System;
using System.Collections.Generic;
using UnityEngine;

namespace UIReborn
{
	public class ObjectPool<T> where T : UIObject
	{
		public ObjectPool(T prefab)
		{
			this.pooled = new Queue<T>();
			this.prefab = prefab;
		}

		public T Pop(Vector2 anchorPos, Transform parent)
		{
			T result;
			if (this.pooled.Count > 0)
			{
				result = this.pooled.Dequeue();
				result.gameObject.SetActive(true);
			}
			else
			{
				result = UnityEngine.Object.Instantiate<T>(this.prefab);
			}
			result.rectTransform.anchoredPosition = anchorPos;
			result.rectTransform.SetParent(parent);
			return result;
		}

		public void Return(T obj)
		{
			obj.gameObject.SetActive(false);
			this.pooled.Enqueue(obj);
		}

		private Queue<T> pooled;

		private T prefab;
	}
}
