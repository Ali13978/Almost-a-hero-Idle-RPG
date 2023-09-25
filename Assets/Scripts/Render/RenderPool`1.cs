using System;
using System.Collections.Generic;
using UnityEngine;

namespace Render
{
	public abstract class RenderPool<T> : IRenderPool
	{
		public RenderPool(Transform parent)
		{
			this.parent = parent;
			this.instances = new List<T>();
			this.atInstance = 0;
			AllRenderPools.all.Add(this);
		}

		public void OnPreFrame()
		{
			this.atInstance = 0;
		}

		public virtual T GetInstance()
		{
			if (this.atInstance == this.instances.Count)
			{
				T item = this.Create();
				this.instances.Add(item);
			}
			return this.instances[this.atInstance++];
		}

		public abstract void OnPostFrame();

		protected abstract T Create();

		protected abstract void Destroy(T instance);

		protected abstract void Activate(T instance);

		protected abstract void Deactivate(T instance);

		protected abstract bool IsActive(T instance);

		protected void DeactivateAllUnused()
		{
			int count = this.instances.Count;
			for (int i = 0; i < this.atInstance; i++)
			{
				T instance = this.instances[i];
				this.Activate(instance);
			}
			for (int j = this.atInstance; j < count; j++)
			{
				T instance2 = this.instances[j];
				if (!this.IsActive(instance2))
				{
					break;
				}
				this.Deactivate(instance2);
			}
		}

		protected void DestroyAllUnused()
		{
			int count = this.instances.Count;
			for (int i = this.atInstance; i < count; i++)
			{
				this.Destroy(this.instances[i]);
			}
			this.instances.RemoveRange(this.atInstance, count - this.atInstance);
		}

		protected List<T> instances;

		protected int atInstance;

		protected Transform parent;
	}
}
