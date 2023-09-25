using System;
using UnityEngine;

namespace Render
{
	public abstract class RenderPoolSpineAnim<T> : RenderPoolSpineAnim where T : SpineAnim, new()
	{
		public RenderPoolSpineAnim(Transform parent) : base(parent)
		{
		}

		protected override SpineAnim Create()
		{
			T t = Activator.CreateInstance<T>();
			t.gameObject.transform.SetParent(this.parent, false);
			return t;
		}
	}
}
