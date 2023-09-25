using System;
using Spine.Unity;
using UnityEngine;

namespace Ui
{
	public class PopupSpine : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToUpdates();
		}

		public override void AahUpdate(float dt)
		{
			this.rectShine.localEulerAngles += new Vector3(0f, 0f, dt * 30f);
		}

		public void FadeInAnim()
		{
			if (this.skeletonGraphic.AnimationState != null)
			{
				this.skeletonGraphic.AnimationState.SetAnimation(0, "in", false);
			}
		}

		public void FadeOutAnim()
		{
			if (this.skeletonGraphic.AnimationState != null)
			{
				this.skeletonGraphic.AnimationState.SetAnimation(0, "out", false);
			}
		}

		public SkeletonGraphic skeletonGraphic;

		public RectTransform rectShine;
	}
}
