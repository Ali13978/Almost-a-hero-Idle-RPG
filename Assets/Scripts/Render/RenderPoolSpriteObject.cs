using System;
using UnityEngine;

namespace Render
{
	public abstract class RenderPoolSpriteObject : RenderPool<SpriteRendererObject>
	{
		public RenderPoolSpriteObject(SpriteRendererObject Prefab, Transform parent) : base(parent)
		{
			this.Prefab = Prefab;
		}

		protected override SpriteRendererObject Create()
		{
			SpriteRendererObject spriteRendererObject = UnityEngine.Object.Instantiate<SpriteRendererObject>(this.Prefab);
			spriteRendererObject.transform.SetParent(this.parent, false);
			return spriteRendererObject;
		}

		public void ChangePrefab(SpriteRendererObject prefab)
		{
			this.Prefab = prefab;
		}

		protected override void Destroy(SpriteRendererObject instance)
		{
			UnityEngine.Object.Destroy(instance);
		}

		protected override void Activate(SpriteRendererObject instance)
		{
			instance.gameObject.SetActive(true);
		}

		protected override void Deactivate(SpriteRendererObject instance)
		{
			instance.gameObject.SetActive(false);
		}

		protected override bool IsActive(SpriteRendererObject instance)
		{
			return instance.gameObject.activeSelf;
		}

		protected SpriteRendererObject Prefab;
	}
}
