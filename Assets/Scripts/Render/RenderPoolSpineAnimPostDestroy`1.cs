using System;
using UnityEngine;

namespace Render
{
	public class RenderPoolSpineAnimPostDestroy<T> : RenderPoolSpineAnim<T> where T : SpineAnim, new()
	{
		public RenderPoolSpineAnimPostDestroy(Transform parent) : base(parent)
		{
		}

		public override void OnPostFrame()
		{
			base.DestroyAllUnused();
		}
	}
}
