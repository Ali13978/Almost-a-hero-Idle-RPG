using System;
using UnityEngine;

namespace Render
{
	public class RenderPoolSpriteRendererContainerPostDeactivate : RenderPoolSpriteRendererContainer
	{
		public RenderPoolSpriteRendererContainerPostDeactivate(Transform parent) : base(parent)
		{
		}

		public override void OnPostFrame()
		{
			base.DeactivateAllUnused();
		}
	}
}
