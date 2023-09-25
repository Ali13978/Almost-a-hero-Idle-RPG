using System;
using UnityEngine;

namespace Render
{
	public class RenderPoolTextPostDeactivate : RenderPoolText
	{
		public RenderPoolTextPostDeactivate(TextContainer Prefab, Transform parent) : base(Prefab, parent)
		{
		}

		public override void OnPostFrame()
		{
			base.DeactivateAllUnused();
		}
	}
}
