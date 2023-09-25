using System;
using UnityEngine;

namespace Render
{
	public abstract class RenderPoolGameObject : RenderPool<GameObject>
	{
		public RenderPoolGameObject(GameObject Prefab, Transform parent) : base(parent)
		{
			this.Prefab = Prefab;
		}

		protected override GameObject Create()
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Prefab);
			gameObject.transform.SetParent(this.parent, false);
			return gameObject;
		}

		public void ChangePrefab(GameObject prefab)
		{
			this.Prefab = prefab;
		}

		protected override void Destroy(GameObject instance)
		{
			UnityEngine.Object.Destroy(instance);
		}

		protected override void Activate(GameObject instance)
		{
			instance.SetActive(true);
		}

		protected override void Deactivate(GameObject instance)
		{
			instance.SetActive(false);
		}

		protected override bool IsActive(GameObject instance)
		{
			return instance.activeSelf;
		}

		protected GameObject Prefab;
	}
}
