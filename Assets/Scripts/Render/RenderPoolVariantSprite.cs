using System;
using UnityEngine;

namespace Render
{
	public abstract class RenderPoolVariantSprite : RenderPool<VariantSpriteRenderer>
	{
		public RenderPoolVariantSprite(VariantSpriteRenderer prefab, Transform parent) : base(parent)
		{
			this.prefab = prefab;
		}

		public void ChangePrefab(VariantSpriteRenderer prefab)
		{
			this.prefab = prefab;
		}

		protected override void Activate(VariantSpriteRenderer instance)
		{
			instance.gameObject.SetActive(true);
		}

		protected override void Deactivate(VariantSpriteRenderer instance)
		{
			instance.gameObject.SetActive(false);
		}

		protected override void Destroy(VariantSpriteRenderer instance)
		{
			UnityEngine.Object.Destroy(instance.gameObject);
		}

		protected override bool IsActive(VariantSpriteRenderer instance)
		{
			return instance.gameObject.activeSelf;
		}

		protected override VariantSpriteRenderer Create()
		{
			return UnityEngine.Object.Instantiate<VariantSpriteRenderer>(this.prefab, this.parent);
		}

		private VariantSpriteRenderer prefab;
	}
}
