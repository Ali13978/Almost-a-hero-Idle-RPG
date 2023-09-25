using System;
using Spine.Unity;
using UnityEngine;

namespace Ui
{
	public class AlchemistSpine : AahMonoBehaviour
	{
		public void SetAnimation(string anim, bool loop, int mixAnimId = 0)
		{
			if (!loop || this.animLast != anim)
			{
				this.animLast = anim;
				this.skeletonGraphic.SetAllDirty();
				if (this.skeletonGraphic.AnimationState != null)
				{
					this.skeletonGraphic.AnimationState.SetAnimation(0, anim, loop);
					this.skeletonGraphic.Update(0f);
				}
				if (this.skeletonGraphic.Skeleton != null)
				{
					this.skeletonGraphic.Skeleton.SetToSetupPose();
				}
			}
		}

		public void Spawn()
		{
			this.SetAnimation("appear", false, 0);
			this.skeletonGraphic.AnimationState.AddAnimation(0, "idle", true, 0f);
		}

		public void End()
		{
			this.SetAnimation("end", false, 0);
		}

		public void Idle()
		{
			this.SetAnimation("idle", true, 0);
		}

		public void Mix(float ratio)
		{
			int num = Mathf.CeilToInt(Mathf.Clamp(ratio, 0f, 1f) * 5f);
			this.SetAnimation("mix_" + num, true, num);
		}

		public SkeletonGraphic skeletonGraphic;

		private string animLast;
	}
}
