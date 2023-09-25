using System;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace Render
{
	public class SmartProjectileRenderer : MonoBehaviour, ISpineAnim
	{
		public void Apply(Spine.Animation anim, float time, bool loop)
		{
			this.projectileSkeleton.skeleton.SetToSetupPose();
			anim.Apply(this.projectileSkeleton.skeleton, time, time, loop, null, 1f, MixBlend.Setup, MixDirection.In);
			this.projectileSkeleton.UpdateTTR();
		}

		public void SetSkin(int index)
		{
			this.projectileSkeleton.Skeleton.SetSkin(index - 1);
			this.projectileSkeleton.Skeleton.SetToSetupPose();
		}

		public void SetColor(Color color)
		{
			this.projectileSkeleton.Skeleton.SetColor(color);
		}

		public void SetAlpha(float a)
		{
			Color color = this.projectileSkeleton.Skeleton.GetColor();
			color.a = a;
			this.projectileSkeleton.Skeleton.SetColor(color);
		}

		public void SetSkin(string skinName)
		{
			throw new NotImplementedException();
		}

		

		public float spinSpeed = 0.4f;

		public SkeletonRenderer projectileSkeleton;
	}
}
