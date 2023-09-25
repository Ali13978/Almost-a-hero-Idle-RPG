using System;
using UnityEngine;

namespace Render
{
	public abstract class RenderPoolText : RenderPool<TextContainer>
	{
		public RenderPoolText(TextContainer Prefab, Transform parent) : base(parent)
		{
			this.Prefab = Prefab;
		}

		protected override TextContainer Create()
		{
			TextContainer textContainer = UnityEngine.Object.Instantiate<TextContainer>(this.Prefab);
			textContainer.transform.SetParent(this.parent, false);
			return textContainer;
		}

		protected override void Destroy(TextContainer instance)
		{
			UnityEngine.Object.Destroy(instance.gameObject);
		}

		protected override void Activate(TextContainer instance)
		{
			instance.canvas.enabled = true;
		}

		protected override void Deactivate(TextContainer instance)
		{
			instance.canvas.enabled = false;
		}

		protected override bool IsActive(TextContainer instance)
		{
			return instance.canvas.enabled;
		}

		protected TextContainer Prefab;
	}
}
