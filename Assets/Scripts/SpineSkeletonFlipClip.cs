using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class SpineSkeletonFlipClip : PlayableAsset, ITimelineClipAsset
{
	public ClipCaps clipCaps
	{
		get
		{
			return ClipCaps.None;
		}
	}

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		ScriptPlayable<SpineSkeletonFlipBehaviour> playable = ScriptPlayable<SpineSkeletonFlipBehaviour>.Create(graph, this.template, 0);
		return playable;
	}

	public SpineSkeletonFlipBehaviour template = new SpineSkeletonFlipBehaviour();
}
