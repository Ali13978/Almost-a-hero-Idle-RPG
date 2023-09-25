using System;
using Spine.Unity;
using UnityEngine;

namespace Ui
{
	public class SkinRevealAnimator : MonoBehaviour
	{
		private void Update()
		{
			if (this.t1)
			{
				this.t1 = false;
				this.Setup(this.t2);
			}
			if (this.play)
			{
				this.play = false;
				this.Play();
			}
		}

		public void Setup(bool isPriced)
		{
			this.priceParent.gameObject.SetActive(isPriced);
			if (isPriced)
			{
				this.skeletonGraph.Skeleton.SetSkin(0);
			}
			else
			{
				this.skeletonGraph.Skeleton.SetSkin(1);
			}
			this.skeletonGraph.Skeleton.SetToSetupPose();
			this.skeletonGraph.AnimationState.ClearTrack(0);
		}

		public void Play()
		{
			this.skeletonGraph.AnimationState.SetAnimation(0, "animation", false);
		}

		public SkeletonGraphic skeletonGraph;

		public RectTransform priceParent;

		private float priceAppearTime;

		public bool t1;

		public bool t2;

		public bool play;
	}
}
