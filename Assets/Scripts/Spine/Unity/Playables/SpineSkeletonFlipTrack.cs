using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Spine.Unity.Playables
{
	[TrackColor(0.855f, 0.8623f, 0.87f)]
	[TrackClipType(typeof(SpineSkeletonFlipClip))]
	[TrackBindingType(typeof(SpinePlayableHandleBase))]
	public class SpineSkeletonFlipTrack : TrackAsset
	{
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return ScriptPlayable<SpineSkeletonFlipMixerBehaviour>.Create(graph, inputCount);
		}

		public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
		{
			base.GatherProperties(director, driver);
		}
	}
}
