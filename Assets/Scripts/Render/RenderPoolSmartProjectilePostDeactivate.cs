using System;
using UnityEngine;

namespace Render
{
	public class RenderPoolSmartProjectilePostDeactivate : RenderPoolSmartProjectile
	{
		public RenderPoolSmartProjectilePostDeactivate(SmartProjectileRenderer prefab, Transform parent) : base(prefab, parent)
		{
		}

		public override void OnPostFrame()
		{
			base.DeactivateAllUnused();
		}
	}
}
