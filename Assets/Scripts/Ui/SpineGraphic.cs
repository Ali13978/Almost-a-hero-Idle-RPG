using System;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace Ui
{
	public class SpineGraphic : AahMonoBehaviour
	{
		public override void Register()
		{
			base.AddToUpdates();
		}

		public override void AahUpdate(float dt)
		{
			if (this.skeletonGraphic.AnimationState != null)
			{
				if (this.scaleToZeroOnComplete)
				{
					this.skeletonGraphic.AnimationState.Complete += this.SpineComplete;
				}
				base.RemoveFromUpdates();
			}
		}

		private void SpineComplete(TrackEntry trackEntry)
		{
			this.skeletonGraphic.transform.localScale = new Vector3(0f, 0f, 0f);
		}

		public SkeletonGraphic skeletonGraphic;

		public bool scaleToZeroOnComplete = true;
	}
}
