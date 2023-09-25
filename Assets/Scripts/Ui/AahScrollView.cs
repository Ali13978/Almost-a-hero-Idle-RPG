using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
	public class AahScrollView : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToInits();
			base.AddToUpdates();
		}

		public override void Init()
		{
			this.scrollRect = base.GetComponent<ScrollRect>();
		}

		public override void AahUpdate(float dt)
		{
			if (Mathf.Abs(this.scrollRect.velocity.x) + Mathf.Abs(this.scrollRect.velocity.y) < 10f)
			{
				this.scrollRect.velocity = new Vector2(0f, 0f);
			}
		}

		private ScrollRect scrollRect;
	}
}
