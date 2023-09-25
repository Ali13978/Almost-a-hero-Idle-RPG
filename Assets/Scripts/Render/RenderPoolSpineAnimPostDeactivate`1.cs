using System;
using UnityEngine;

namespace Render
{
	public class RenderPoolSpineAnimPostDeactivate<T> : RenderPoolSpineAnim<T> where T : SpineAnim, new()
	{
		public RenderPoolSpineAnimPostDeactivate(Transform parent) : base(parent)
		{
		}

		public override void OnPostFrame()
		{
			base.DeactivateAllUnused();
		}
	}
}
