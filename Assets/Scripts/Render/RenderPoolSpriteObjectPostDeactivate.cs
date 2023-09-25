using System;
using UnityEngine;

namespace Render
{
	public class RenderPoolSpriteObjectPostDeactivate : RenderPoolSpriteObject
	{
		public RenderPoolSpriteObjectPostDeactivate(SpriteRendererObject Prefab, Transform parent) : base(Prefab, parent)
		{
		}

		public override void OnPostFrame()
		{
			base.DeactivateAllUnused();
		}
	}
}
