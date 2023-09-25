using System;
using UnityEngine;

namespace Render
{
	public class RenderPoolVariantSpriteDestroy : RenderPoolVariantSprite
	{
		public RenderPoolVariantSpriteDestroy(VariantSpriteRenderer Prefab, Transform parent) : base(Prefab, parent)
		{
		}

		public override void OnPostFrame()
		{
			base.DestroyAllUnused();
		}
	}
}
