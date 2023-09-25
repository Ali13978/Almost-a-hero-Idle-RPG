using System;
using UnityEngine;

namespace Render
{
	public class RenderPoolSmartProjectilePostDestroy : RenderPoolSmartProjectile
	{
		public RenderPoolSmartProjectilePostDestroy(SmartProjectileRenderer prefab, Transform parent) : base(prefab, parent)
		{
		}

		public override void OnPostFrame()
		{
			base.DestroyAllUnused();
		}
	}
}
