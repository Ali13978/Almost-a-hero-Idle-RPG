using System;
using UnityEngine;

namespace Render
{
	public class RenderPoolGameObjectPostDeactivate : RenderPoolGameObject
	{
		public RenderPoolGameObjectPostDeactivate(GameObject Prefab, Transform parent) : base(Prefab, parent)
		{
		}

		public override void OnPostFrame()
		{
			base.DeactivateAllUnused();
		}
	}
}
