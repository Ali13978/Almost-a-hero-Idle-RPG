using System;
using UnityEngine;

namespace Render
{
	public abstract class RenderPoolSpineAnim : RenderPool<SpineAnim>
	{
		public RenderPoolSpineAnim(Transform parent) : base(parent)
		{
		}

		protected override void Activate(SpineAnim instance)
		{
			instance.gameObject.SetActive(true);
		}

		protected override void Deactivate(SpineAnim instance)
		{
			instance.gameObject.SetActive(false);
		}

		protected override void Destroy(SpineAnim instance)
		{
			UnityEngine.Object.Destroy(instance.gameObject);
		}

		protected override bool IsActive(SpineAnim instance)
		{
			return instance.gameObject.activeSelf;
		}
	}
}
