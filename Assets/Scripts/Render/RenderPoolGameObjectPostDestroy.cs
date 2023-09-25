using System;
using UnityEngine;

namespace Render
{
	public class RenderPoolGameObjectPostDestroy : RenderPoolGameObject
	{
		public RenderPoolGameObjectPostDestroy(GameObject Prefab, Transform parent) : base(Prefab, parent)
		{
		}

		public override void OnPostFrame()
		{
			base.DestroyAllUnused();
		}
	}
}
