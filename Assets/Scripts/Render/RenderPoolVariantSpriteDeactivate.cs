using System;
using UnityEngine;

namespace Render
{
	public class RenderPoolVariantSpriteDeactivate : RenderPoolVariantSprite
	{
		public RenderPoolVariantSpriteDeactivate(VariantSpriteRenderer Prefab, Transform parent) : base(Prefab, parent)
		{
		}

		public override void OnPostFrame()
		{
			base.DeactivateAllUnused();
		}
	}
}
