using System;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace Render
{
	public class SpineAnim : ISpineAnim
	{
		protected SpineAnim(GameObject prefab)
		{
			this.gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab);
			this.skeletonAnim = this.gameObject.GetComponentInChildren<SkeletonAnimation>();
			this.skeleton = this.skeletonAnim.Skeleton;
			if (this.skeleton == null)
			{
				throw new Exception();
			}
		}

		public SpineAnim(SkeletonAnimation instance)
		{
			this.skeletonAnim = instance;
			this.skeleton = this.skeletonAnim.Skeleton;
		}

		public GameObject gameObject { get; set; }

		public void Apply(Spine.Animation anim, float time, bool loop)
		{
			this.skeleton.SetToSetupPose();
			anim.Apply(this.skeleton, time, time, loop, null, 1f, MixBlend.Setup, MixDirection.In);
			this.skeletonAnim.UpdateTTR();
		}

		public void SetColor(Color color)
		{
			this.skeleton.SetColor(color);
		}

		public Color GetColor()
		{
			return this.skeleton.GetColor();
		}

		public void SetAlpha(float a)
		{
			this.skeleton.A = a;
		}

		public void SetSkin(string skinName)
		{
			if (this.skeleton.skin != null && this.skeleton.skin.name == skinName)
			{
				return;
			}
			this.skeleton.SetSkin(skinName);
		}

		public void SetSkin(int index)
		{
			int num = (index >= this.skeleton.Data.Skins.Count) ? 0 : index;
			if (num == this.lastSkinIndex)
			{
				return;
			}
			this.skeleton.SetSkin(num);
			this.lastSkinIndex = num;
		}

		public void SetFlip(bool isFlipped)
		{
			this.skeleton.ScaleX = (float)((!isFlipped) ? 1 : -1);
		}

		public void SetRootBone(string boneName)
		{
			this.rootBone = this.skeletonAnim.Skeleton.FindBone(boneName);
		}

		public Vector2 GetRootBonePosition()
		{
			if (this.rootBone == null)
			{
				return Vector2.zero;
			}
			return this.skeletonAnim.transform.TransformPoint(new Vector3(this.rootBone.worldX, this.rootBone.worldY));
		}

		public Vector2 GetBoneWorldPosition(string boneName)
		{
			Bone bone = this.skeletonAnim.Skeleton.FindBone(boneName);
			return this.skeletonAnim.transform.TransformPoint(new Vector3(bone.worldX, bone.worldY));
		}

		private GameObject prefab;

		private SkeletonAnimation skeletonAnim;

		private Skeleton skeleton;

		private Bone rootBone;

		private int lastSkinIndex = -1;
	}
}
