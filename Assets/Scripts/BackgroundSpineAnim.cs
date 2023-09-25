using System;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using UnityEngine;

public class BackgroundSpineAnim : MonoBehaviour
{
	public void Init()
	{
		this.skeletons = new List<Skeleton>();
		this.animations = new List<Spine.Animation>();
		foreach (BackgroundAnimationData backgroundAnimationData in this.backgroundAnimations)
		{
			Skeleton skeleton = backgroundAnimationData.skeletonAnimation.Skeleton;
			if (skeleton == null)
			{
				throw new Exception();
			}
			this.skeletons.Add(skeleton);
			this.animations.Add(skeleton.data.FindAnimation(this.animationName));
		}
	}

	public void Apply(float time, bool loop, Color color)
	{
		for (int i = 0; i < this.backgroundAnimations.Length; i++)
		{
			BackgroundAnimationData backgroundAnimationData = this.backgroundAnimations[i];
			Skeleton skeleton = this.skeletons[i];
			Spine.Animation animation = this.animations[i];
			SkeletonAnimation skeletonAnimation = backgroundAnimationData.skeletonAnimation;
			skeleton.SetToSetupPose();
			if (backgroundAnimationData.referenceTime == 0f)
			{
				backgroundAnimationData.referenceTime = time;
			}
			float num;
			if (backgroundAnimationData.delay > time - backgroundAnimationData.referenceTime)
			{
				num = 0f;
			}
			else
			{
				if (backgroundAnimationData.delay > 0f)
				{
					backgroundAnimationData.referenceTime = time;
					backgroundAnimationData.delay = -1f;
				}
				if (this.backgroundAnimations[i].timeBetweenLoops > 0f)
				{
					num = (time - backgroundAnimationData.referenceTime) % (backgroundAnimationData.timeBetweenLoops + animation.duration);
					if (num > animation.duration)
					{
						num = 0f;
					}
				}
				else
				{
					num = time * this.animationSpeed + backgroundAnimationData.timeOffset;
				}
			}
			animation.Apply(skeleton, num, num, loop, null, 1f, MixBlend.Replace, MixDirection.In);
			skeleton.SetColor(color);
			skeletonAnimation.UpdateTTR();
		}
	}

	public void SetSkin(int index)
	{
		if (this.currentSkin != index)
		{
			for (int i = 0; i < this.backgroundAnimations.Length; i++)
			{
				Skeleton skeleton = this.skeletons[i];
				skeleton.SetSkin(index);
			}
			this.currentSkin = index;
		}
	}

	public float animationSpeed = 1f;

	public BackgroundAnimationData[] backgroundAnimations;

	[SpineAnimation("", "", true, false)]
	public string animationName;

	private List<Skeleton> skeletons;

	private List<Spine.Animation> animations;

	private int currentSkin;
}
