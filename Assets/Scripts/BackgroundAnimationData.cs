using System;
using Spine.Unity;

[Serializable]
public class BackgroundAnimationData
{
	public SkeletonAnimation skeletonAnimation;

	public float timeOffset;

	public float timeBetweenLoops;

	public float delay;

	[NonSerialized]
	public float referenceTime;
}
