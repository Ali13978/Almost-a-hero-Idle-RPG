using System;
using Spine.Unity;
using UnityEngine;

namespace DynamicLoading
{
	[CreateAssetMenu(fileName = "HeroBundle", menuName = "Bundles/Hero")]
	public class HeroBundle : ScriptableObject
	{
		public GameObject prefab;

		public SkeletonDataAsset animation;

		public AudioClip[] SelectedAudioClips;

		public AudioClip[] UpgradeItemAudioClips;
	}
}
