using System;
using UnityEngine;

namespace Render
{
	public abstract class RenderPoolSpriteRendererContainer : RenderPool<SpriteRendererContainer>
	{
		public RenderPoolSpriteRendererContainer(Transform parent) : base(parent)
		{
		}

		protected override SpriteRendererContainer Create()
		{
			SpriteRendererContainer spriteRendererContainer = new SpriteRendererContainer();
			spriteRendererContainer.gameObject.transform.SetParent(this.parent, false);
			return spriteRendererContainer;
		}

		protected override void Activate(SpriteRendererContainer instance)
		{
			instance.gameObject.SetActive(true);
		}

		protected override void Deactivate(SpriteRendererContainer instance)
		{
			instance.gameObject.SetActive(false);
		}

		protected override void Destroy(SpriteRendererContainer instance)
		{
			UnityEngine.Object.Destroy(instance.gameObject);
		}

		protected override bool IsActive(SpriteRendererContainer instance)
		{
			return instance.gameObject.activeSelf;
		}
	}
}
